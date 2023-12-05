using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DealerAPI.Data;
using Dealer.Model;
using Dealer.Model.DTO;
using DealerAPI.Data;

namespace MyAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PaymentPayDto>>> GetDuePayments()
        {
            try
            {
                _logger.LogInformation("Getting Due Payments");

                var currentDate = DateTime.UtcNow;

                var duePayments = await _db.Payment
                    .Include(p => p.Car)
                                        .Include(p => p.BankDetail)
                    .ToListAsync(); // Retrieve all payments from the database

                var filteredDuePayments = duePayments
                    .Where(p => DateTime.Compare(currentDate.Date ,p.DueDate.Date) < 0 &&
                                (p.DueDate.Date-currentDate.Date).Days <= 60)
                    .ToList(); // Filter the payments locally

                var paymentDtos = filteredDuePayments.Select(p => new PaymentPayDto
                {
                    Id = p.Id,
                    Amount_Due = p.Amount_Due,
                    CarId = p.CarId,
                    CarName = p.CarName,
                    Variant = p.Variant,
                    DaysLeft = $"{(p.DueDate - currentDate).Days}/60",

                    DueDate = p.DueDate,
                    StartDate = p.StartDate,
                    AmountPaid = p.AmountPaid,
                    ProcessingCharges = p.ProcessingCharges,
                    Name =              p.Name,
                    AccountNumber = p.AccountNumber,
                    BankName = p.BankName,
                    IFSCCode = p.IFSCCode

                }).ToList();

                return Ok(paymentDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting due payments: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("upcoming")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PaymentPayDto>>> GetUpcomingPayments()
        {
            try
            {
                _logger.LogInformation("Getting Upcoming Payments");

                var currentDate = DateTime.UtcNow;

                var upcomingPayments = await _db.Payment
                    .Include(p => p.Car)
                                        .Include(p => p.BankDetail)

                    .ToListAsync(); // Retrieve all payments from the database
                var filteredDuePayments = upcomingPayments
                    .Where(p => DateTime.Compare(currentDate.Date ,p.DueDate.Date) < 0 &&
                                (p.DueDate.Date-currentDate.Date).Days > 60)
                    .ToList();

                var paymentDtos = filteredDuePayments.Select(p => new PaymentPayDto
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
            catch (Exception ex)
            {
                _logger.LogError($"Error getting upcoming payments: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("Status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PaymentHistoryDto>>> GetPaymentStatus()
        {
            try
            {
                _logger.LogInformation("Getting Payments status");

                var currentDate = DateTime.UtcNow;

                var PaymentStat = await _db.Payment
                    .Where(p => p.PaymentStatus != null)
                    .Include(p => p.Car)
                    .ToListAsync(); // Retrieve all payments from the database


                var paymentHistoryDtos = PaymentStat.Select(p => new PaymentHistoryDto
                {

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
                _logger.LogError($"Error getting upcoming payments: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }





        [HttpGet("details/{paymentId}")]
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
                    DaysLeft = $"{(payment.DueDate-currentDate).Days}/60",

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




        //public async Task<ActionResult<PaymentDto>> GetPaymentDetailsWithBankDetails(string paymentId)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Getting Payment Details with Bank Details for Payment ID: {paymentId}");

        //        var payment = await _db.Payment
        //            .Include(p => p.Car)
        //            .Include(p => p.BankDetails)
        //            .FirstOrDefaultAsync(p => p.Id == paymentId);

        //        if (payment == null)
        //        {
        //            _logger.LogWarning($"Payment with ID {paymentId} not found");
        //            return NotFound();
        //        }

        //        var paymentDto = new PaymentDto
        //        {
        //            Id = payment.Id,

        //            Amount_Due = payment.Amount_Due,
        //            CarId = payment.CarId,
        //            CarName = payment.Car?.CarName,
        //            Variant = payment.Car?.Variant,
        //            DueDate = payment.DueDate,
        //            StartDate = payment.StartDate,
        //            AmountPaid = payment.AmountPaid,
        //            DaysLeft = $"{(payment.StartDate - payment.DueDate).Days}/60",
        //            ProcessingCharges = payment.ProcessingCharges,
        //            PaymentStatus = Enum.GetName(typeof(paymentStatus), payment.PaymentStatus),
        //            BankDetails = payment.BankDetails.Select(b => new BankDetailDto
        //            {
        //                RepaymentDetailId = b.RepaymentDetailId,
        //                Name = b.Name,
        //                AccountNumber = b.AccountNumber,
        //                IFSCCode = b.IFSCCode,
        //                BankName = b.BankName
        //            }).ToList()
        //        };

        //        return Ok(paymentDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error getting payment details: {ex.Message}");
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}



        //[HttpGet("all")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAllPayments()
        //{
        //    try
        //    {
        //        _logger.LogInformation("Getting All Payments");

        //        var allPayments = await _db.Payment
        //            .Include(p => p.Car)
        //            .ToListAsync(); // Retrieve all payments from the database

        //        var paymentDtos = allPayments.Select(p => new PaymentDto
        //        {
        //            CarName = p.Car?.CarName,
        //            Variant = p.Car?.Variant,
        //            AmountPaid = p.AmountPaid,
        //            CarId = p.CarId,
        //            PaymentStatus = Enum.GetName(typeof(paymentStatus), p.PaymentStatus)
        //            // Convert enum to string representation
        //        }).ToList();

        //        return Ok(paymentDtos);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error getting all payments: {ex.Message}");
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}


        //[HttpGet("{id:int}", Name="GetSample")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult <SampleDTO> GetSample(int id)
        //{
        //    if(id == 0)
        //    {
        //        _logger.LogError("GetSample Error with Id" + id);
        //        return BadRequest();
        //    }
        //    var sample = SampleStore.SampleList.FirstOrDefault(u => u.Id == id);
        //    if(sample == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(sample);
        //}

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public ActionResult<Sample> CreateSample([FromBody]SampleDTO sample)
        //{
        //    if(SampleStore.SampleList.FirstOrDefault(u => u.Name.ToLower() == sample.Name.ToLower()) != null)
        //    {
        //        ModelState.AddModelError("customerror", "User Already Exists");
        //        return BadRequest(ModelState);
        //    }
        //    if(sample == null)
        //    {
        //        return BadRequest();
        //    }
        //    if(sample.Id > 0)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //    sample.Id = SampleStore.SampleList.OrderByDescending(u => u.Id).FirstOrDefault().Id+1;
        //    SampleStore.SampleList.Add(sample);
        //    return CreatedAtRoute("GetSample", new { id = sample.Id }, sample);
        //}

    }
}

