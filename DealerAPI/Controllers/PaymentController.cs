using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DealerAPI.Data;
using Dealer.Model;
using Dealer.Model.DTO;
using DealerAPI.Data;
using System.Security.Claims;

namespace MyAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly ApplicationDbContext _db;
        public PaymentController(ILogger<PaymentController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        [HttpGet("due")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PaymentPayDto>>> GetDuePayments()
        {
            try
            {
                _logger.LogInformation("Getting Due Payments");

                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(userIdString, out int userId))
                {
                    var currentDate = DateTime.UtcNow;

                    var userCars = await _db.Cars.Where(c => c.UserId == userId).ToListAsync();
                    var carIds = userCars.Select(car => car.CarId).ToList();
                    var duePayments = await _db.Payment
    .Include(p => p.Car)
    .Include(p => p.BankDetail)
    .Where(p => carIds.Contains(p.CarId))
    .ToListAsync();

                    var filteredDuePayments = duePayments
                        .Where(p => DateTime.Compare(currentDate.Date, p.DueDate.Date) < 0 &&
                                    (p.DueDate.Date - currentDate.Date).Days <= 60)
                        .ToList(); //Retrieve filtered payments from the database

                    var paymentDtos = duePayments.Select(p => new PaymentPayDto
                    {
                        Id = p.Id,
                        Amount_Due = p.Amount_Due,
                        CarId = p.CarId,
                        CarName = p.CarName,
                        Variant = p.Variant,
                        DueDate = p.DueDate,
                        DaysLeft = $"{(p.DueDate - currentDate).Days}/60",

                        StartDate = p.StartDate,
                        AmountPaid = p.AmountPaid,
                        ProcessingCharges = p.ProcessingCharges,
                        Name = p.Name,
                        AccountNumber = p.AccountNumber,
                        BankName = p.BankName,
                        IFSCCode = p.IFSCCode,
                        UserId = p.Userid

                        // Populate PaymentPayDto properties here...
                    }).ToList();

                    return Ok(paymentDtos);
                }
                else
                {
                    // Handle the case where the user ID from the claim cannot be parsed as an integer
                    return BadRequest("Invalid user ID");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting due payments: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("upcoming")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PaymentPayDto>>> GetUpcomingPayments()
        {
            try
            {
                _logger.LogInformation("Getting Upcoming Payments");

                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(userIdString, out int userId))
                {
                    var currentDate = DateTime.UtcNow;

                    var userCars = await _db.Cars.Where(c => c.UserId == userId).ToListAsync();
                    var carIds = userCars.Select(car => car.CarId).ToList();
                    var upcomingPayments = await _db.Payment
                        .Include(p => p.Car)
                        .Include(p => p.BankDetail)
                        .Where(p => carIds.Contains(p.CarId))
                        .ToListAsync();

                    var filteredUpcomingPayments = upcomingPayments
                        .Where(p => DateTime.Compare(currentDate.Date, p.DueDate.Date) < 0 &&
                                    (p.DueDate.Date - currentDate.Date).Days > 60)
                        .ToList();

                    var paymentDtos = filteredUpcomingPayments.Select(p => new PaymentPayDto
                    {
                        Id = p.Id,
                        Amount_Due = p.Amount_Due,
                        CarId = p.CarId,
                        CarName = p.CarName,
                        Variant = p.Variant,
                        DueDate = p.DueDate,
                        DaysLeft = $"{(p.DueDate - currentDate).Days}/60",

                        StartDate = p.StartDate,
                        AmountPaid = p.AmountPaid,
                        ProcessingCharges = p.ProcessingCharges,
                        Name = p.Name,
                        AccountNumber = p.AccountNumber,
                        BankName = p.BankName,
                        IFSCCode = p.IFSCCode
                    }).ToList();

                    return Ok(paymentDtos);
                }
                else
                {
                    // Handle the case where the user ID from the claim cannot be parsed as an integer
                    return BadRequest("Invalid user ID");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting upcoming payments: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpGet("status")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PaymentHistoryDto>>> GetPaymentStatus()
        {
            try
            {
                _logger.LogInformation("Getting Payment Status");

                var currentDate = DateTime.UtcNow;

                var paymentStatusList = await _db.Payment
                    .Where(p => p.PaymentStatus != null)
                    .Include(p => p.Car)
                    .ToListAsync();

                var paymentHistoryDtos = paymentStatusList.Select(p => new PaymentHistoryDto
                {Id = p.Id,

                    Amount_Due = p.Amount_Due,
                    CarId = p.CarId,
                    CarName = p.CarName,
                    Variant = p.Variant,
                    PaymentStatus = p.PaymentStatus
                }).ToList();

                return Ok(paymentHistoryDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting payment status: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("details/{paymentId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaymentPayDto>> GetPaymentDetailsWithBankDetails(int paymentId)
        {
            try
            {
                var currentDate = DateTime.UtcNow;
                _logger.LogInformation($"Getting Payment Details with Bank Details for Payment ID: {paymentId}");

                var payment = await _db.Payment
                    .Include(p => p.Car)
                    .Include(p => p.BankDetail)
                    .FirstOrDefaultAsync(p => p.Id == paymentId);

                if (payment == null)
                {
                    _logger.LogWarning($"Payment with ID {paymentId} not found");
                    return NotFound();
                }


                var paymentDto = new PaymentPayDto
                {
                    Id = paymentId,
                    Amount_Due = payment.Amount_Due,
                    CarId = payment.CarId,
                    CarName = payment.CarName,
                    Variant = payment.Variant,
                    DaysLeft = $"{(payment.DueDate - currentDate).Days}/60",

                    DueDate = payment.DueDate,
                    StartDate = payment.StartDate,
                    AmountPaid = payment.AmountPaid,
                    ProcessingCharges = payment.ProcessingCharges,
                    Name = payment.Name,
                    AccountNumber = payment.AccountNumber,
                    BankName = payment.BankName,
                    IFSCCode = payment.IFSCCode



                };

                return Ok(paymentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting payment details: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }


    }
}



