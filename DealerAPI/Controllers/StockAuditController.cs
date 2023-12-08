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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StockAuditDto>>> GetUpcomingAudits()
        {
            try
            {
                _logger.LogInformation("Getting Upcoming Audits");
                var currentDate = DateTime.Now;
                var upcomingAudits = await _db.StockAudits
                    .Include(a => a.Car)
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Return an appropriate error response
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }


        [HttpGet("pending")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StockAuditDto>>> GetPendingAudits()
        {

            try
            {
                _logger.LogInformation("Getting Pending Audits");
                var currentDate = DateTime.Now;
                var PendingAudits = await _db.StockAudits
                    .Include(a => a.Car)
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Return an appropriate error response
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("StockStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StockDto>> GetStockStatus()
        {
            try
            {
                _logger.LogInformation("Getting Stock Status");


                var stock = await _db.StockAudits
                    .Include(p => p.Car).ToListAsync();


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
            catch (Exception ex)
            {
                _logger.LogError($"Error getting payment details: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
