using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DealerAPI.Data;
using Dealer.Model;
using Dealer.Model.DTO;
using DealerAPI.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DealerAPI.Models.DTO;

namespace MyAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockAuditController : ControllerBase
    {
        private readonly ILogger<StockAuditController> _logger;
        private readonly ApplicationDbContext _db;
        public StockAuditController(ILogger<StockAuditController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet("upcoming")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StockAuditDto>>> GetUpcomingAudits()
        {
            try
            {
                _logger.LogInformation("Getting Upcoming Audits");

                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(userIdString, out int userId))
                {
                    var currentDate = DateTime.UtcNow;

                    var userCars = await _db.Cars.Where(c => c.UserId == userId).ToListAsync();
                    var carIds = userCars.Select(car => car.CarId).ToList();








                    var upcomingAudits = await _db.StockAudits
                        .Include(a => a.Car)
                            .Where(p => carIds.Contains(p.CarId))
                        .ToListAsync();

                    var filterUpcomingAudit = upcomingAudits.Where(a => (a.AuditDate.Date - currentDate.Date).Days >= 15 && a.AuditDate.Date > currentDate.Date).ToList();
                    var stockAuditDto = filterUpcomingAudit.Select(a => new StockAuditDto

                    {
                        CarId = a.CarId,
                        CarName = a.Car.CarName,
                        Variant = a.Car.Variant,
                        DaysLeftToVerify = $"{(int)(a.AuditDate - currentDate).Days} days left to verify",

                    })
                        .ToList();

                    return Ok(stockAuditDto);
                }
                else
                {
                    // Handle the case where the user ID from the claim cannot be parsed as an integer
                    return BadRequest("Invalid user ID");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Return an appropriate error response
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }



        [HttpGet("pending")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StockAuditDto>>> GetPendingAudits()
        {

            try
            {
                _logger.LogInformation("Getting Pending Audits");
                var currentDate = DateTime.Now;
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(userIdString, out int userId))
                {


                    var userCars = await _db.Cars.Where(c => c.UserId == userId).ToListAsync();
                    var carIds = userCars.Select(car => car.CarId).ToList();


                    var PendingAudits = await _db.StockAudits
                    .Include(a => a.Car)
                     .Where(p => carIds.Contains(p.CarId))
                    .ToListAsync();

                    var filterPendingAudit = PendingAudits.Where(a => (a.AuditDate.Date - currentDate.Date).Days < 15 && a.AuditDate.Date > currentDate.Date).ToList();
                    var stockAuditDto = filterPendingAudit.Select(a => new StockAuditDto

                    {
                        CarId = a.CarId,
                        CarName = a.Car.CarName,
                        Variant = a.Car.Variant,
                        DaysLeftToVerify = $"{(int)(a.AuditDate - currentDate).Days} days left to verify",

                    })
                        .ToList();

                    return Ok(stockAuditDto);
                }
                else
                {
                    // Handle the case where the user ID from the claim cannot be parsed as an integer
                    return BadRequest("Invalid user ID");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Return an appropriate error response
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        //New Update
        [HttpGet("StockStatus")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StockDto>> GetStockStatus()
        {
            try
            {
                _logger.LogInformation("Getting Stock Status");

                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(userIdString, out int userId))
                {


                    var userCars = await _db.Cars.Where(c => c.UserId == userId).ToListAsync();
                    var carIds = userCars.Select(car => car.CarId).ToList();


                    var stock = await _db.StockAudits
                    .Include(p => p.Car).Where(p => carIds.Contains(p.CarId)).ToListAsync();


                    if (stock == null)
                    {

                        return NotFound();
                    }


                    var stockDto = stock.Select(p => new StockDto
                    {
                        CarName = p.Car.CarName,
                        Variant = p.Car.Variant,
                        CarId = p.Car.CarId,
                        AuditDate = p.AuditDate,
                        Status = p.Status,


                    }).ToList();


                    return Ok(stockDto);
                }

                else
                {
                    // Handle the case where the user ID from the claim cannot be parsed as an integer
                    return BadRequest("Invalid user ID");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting payment details: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("addresses")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetUserAddresses()
        {
            try
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(userIdString, out int userId))
                {
                    var userAddresses = await _db.RegisterAddresses
                        .Where(a => a.IdU == userId) // Assuming UserId is a property in your Address entity
                        .Select(a => new AddressDto
                        {
                            Id = a.Id,
                            Address = a.Address,
                            AddressType = a.AddressType // Assuming AddressType is a property in your Address entity
                        })
                        .ToListAsync();

                    return Ok(userAddresses);
                }
                else
                {
                    return BadRequest("Invalid user ID");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving user addresses: {ex.Message}");
            }
        }


    }
}
