using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


using Dealer.Model.DTO;
using DealerAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Dealer.Model;
using System.Security.Claims;

namespace MyAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcurementController : ControllerBase
    {
        private readonly ILogger<ProcurementController> _logger;
        private readonly ApplicationDbContext _db;
        public ProcurementController(ILogger<ProcurementController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet("filter")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProcurementFilterDto>>> GetFilter()
        {
            try
            {
                _logger.LogInformation("Getting filters");

                var filter = await _db.ProcurementFilters.ToListAsync();
                return Ok(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching sports: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("Procurement")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProcurementDto>>> FilterProcurement(int? Id)
        {
            try
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int userId))
                {
                    IQueryable<ProcDetails> procurementQuery = _db.procDetails
                        .Include(p => p.Payment)
                            .ThenInclude(pay => pay.Car)
                        .Where(p => p.Payment.Car.UserId == userId); // Filter by the user's cars

                    if (!Id.HasValue)
                    {
                        _logger.LogInformation("Getting Procurement");
                        // Retrieve all procurements if no filter is selected
                        var allProcurements = await procurementQuery
                            .Select(p => new ProcurementDto
                            {
                                FilterId = p.FilterId,
                                PurchaseId = p.CarId,
                                CarName = p.Payment.CarName,
                                Variant = p.Payment.Variant,
                                Amount_due = p.Due_Amount,
                                Amount_paid = p.Paid_Amount,
                                Facility_Availed = p.Facility_Availed,
                                Invoice_Charges = p.Invoice_Charges,
                                Processing_charges = p.ProcessingCharges,
                                // Add the specific procurement field you want to include
                            })
                            .ToListAsync();

                        return Ok(allProcurements);
                    }

                    // Retrieve procurements filtered by ID
                    var ProcurementFiltered = await procurementQuery
                        .Where(p => p.FilterId == Id)
                        .Select(p => new ProcurementDto
                        {
                            FilterId = p.FilterId,
                            PurchaseId = p.CarId,
                            CarName = p.Payment.CarName,
                            Variant = p.Payment.Variant,
                            Amount_due = p.Due_Amount,
                            Amount_paid = p.Paid_Amount,
                            Facility_Availed = p.Facility_Availed,
                            Invoice_Charges = p.Invoice_Charges,
                            Processing_charges = p.ProcessingCharges,
                            // Add the specific procurement field you want to include
                        })
                        .ToListAsync();

                    if (!ProcurementFiltered.Any())
                    {
                        return NotFound(); // Return 404 if no procurements found for the given ID
                    }

                    return Ok(ProcurementFiltered);
                }
                else
                {
                    // Handle the case where the user ID from the claim cannot be parsed as an integer
                    return BadRequest("Invalid user ID");
                }
            }
            catch (Exception ex)
            {
                // Log and handle exceptions appropriately
                // For simplicity, returning a 500 Internal Server Error here
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }


        [HttpGet("ProcurementStatus")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProcurementStatusDto>>> GetProcurementStatus()
        {
            try
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int userId))
                {
                    _logger.LogInformation("Getting Procurement Status");

                    var procurementStatus = await _db.procDetails
                        .Include(c => c.Payment)
                            .ThenInclude(c => c.Car)
                        .Where(c => c.Status != null && c.Payment.Car.UserId == userId) // Filter by user ID
                        .Select(c => new ProcurementStatusDto
                        {
                            CarName = c.Payment.CarName,
                            Variant = c.Payment.Variant,
                            PurchaseId = c.CarId,
                            Status = c.Status,
                            Purchased_Amount = c.Purchased_Amount
                        })
                        .ToListAsync();

                    return Ok(procurementStatus);
                }
                else
                {
                    return BadRequest("Invalid user ID");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching cars with status: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        //new Change
        [HttpGet("ProcurementClosed")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProcurementColsedDto>>> ProcurementClosed()
        {
            try
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int userId))
                {
                    _logger.LogInformation("Getting Procurement Closed");

                    var procurementclosed = await _db.procDetails
                        .Include(c => c.Payment)
                            .ThenInclude(c => c.Car)
                        .Where(c => c.ClosedOn != null && c.Payment.Car.UserId == userId) // Filter by user ID
                        .Select(c => new ProcurementColsedDto
                        {
                            CarName = c.Payment.CarName,
                            Variant = c.Payment.Variant,
                            Amount_paid = c.Paid_Amount,
                            ColsedOn = c.ClosedOn,
                            PurchaseId = c.CarId
                        })
                        .ToListAsync();

                    return Ok(procurementclosed);
                }
                else
                {
                    return BadRequest("Invalid user ID");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching cars with status: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }


    }
}
