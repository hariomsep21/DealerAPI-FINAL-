using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Dealer.Model.DTO;
using Dealer.Model;
using DealerAPI.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MyAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseVehicleController : ControllerBase
    {
        private readonly ILogger<PurchaseVehicleController> _logger;
        private readonly ApplicationDbContext _db;
        public PurchaseVehicleController(ILogger<PurchaseVehicleController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        [HttpGet("VehicleRecord")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CarStatusDto>>> GetCarsWithStatus()
        {
            try
            {
                _logger.LogInformation("Getting Cars with Status");
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(userIdString, out int userId))
                {
                    var currentDate = DateTime.UtcNow;

                    var userCars = await _db.Cars.Where(c => c.UserId == userId).ToListAsync();
                    var carIds = userCars.Select(car => car.CarId).ToList();


                    var carsWithStatus = await _db.Cars
                    .Where(c => c.vehiclerecords.Any()) // Filter out cars without vehicle records
                    .Where(c => carIds.Contains(c.CarId))
                    .Include(c => c.vehiclerecords)
                    .Select(c => new CarStatusDto
                    {

                        CarName = c.CarName,
                        Variant = c.Variant,
                        PurchaseId = c.CarId,


                        // Determine if any record triggers action required
                        ActionRequired = DetermineActionRequired(c.vehiclerecords)
                    })
                    .ToListAsync();

                    return Ok(carsWithStatus);
                }
                else
                {
                    // Handle the case where the user ID from the claim cannot be parsed as an integer
                    return BadRequest("Invalid user ID");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching cars with status: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        // Method to determine action required based on vehicle records
        private static string DetermineActionRequired(IEnumerable<Vehiclerecord> vehicleRecords)
        {
            if (vehicleRecords != null && vehicleRecords.Any())
            {
                foreach (var record in vehicleRecords)
                {
                    if (!record.Challan || !record.RcStatus || !record.Fitness ||
                        !record.OwnerName || !record.Hypothecation || !record.Blacklist)
                    {
                        return "Action Required"; // Return if any record indicates action is required
                    }
                }
            }

            return "No Action Required"; // Return if no action is required for any record
        }


        [HttpGet("VehicleRecord/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<VehicleRecordsDto>> GetVehicleRecordStatus(int id)
        {
            try
            {
                _logger.LogInformation($"Getting Vehicle Record Status with ID: {id}");
                var vehicleRecord = await _db.VehicleRecords
                    .Include(vr => vr.Car)
                    .FirstOrDefaultAsync(vr => vr.Id == id);

                if (vehicleRecord == null)
                {
                    return NotFound(); // Return 404 if vehicle record is not found
                }

                // Determine which status has failed
                var failedStatus = DetermineFailedStatus(vehicleRecord);

                // Create a DTO to represent the vehicle record status with action required message
                var vehicleRecordStatusDto = new VehicleRecordsDto
                {
                    Challan = vehicleRecord.Challan,
                    RcStatus = vehicleRecord.RcStatus,
                    Fitness = vehicleRecord.Fitness,
                    OwnerName = vehicleRecord.OwnerName,
                    Hypothecation = vehicleRecord.Hypothecation,
                    Blacklist = vehicleRecord.Blacklist,
                    CarName = vehicleRecord.CarName,
                    PurchaseId = vehicleRecord.CId,
                    Variant = vehicleRecord.Variant,


                };

                return Ok(vehicleRecordStatusDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching vehicle record status: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Method to determine which status has failed
        private static string DetermineFailedStatus(Vehiclerecord vehicleRecord)
        {
            if (!vehicleRecord.Challan)
            {
                return "Challan";
            }
            else if (!vehicleRecord.RcStatus)
            {
                return "RC Status";
            }
            else if (!vehicleRecord.Fitness)
            {
                return "Fitness";
            }
            else if (!vehicleRecord.Fitness)
            {
                return "OwnerName";
            }
            else if (!vehicleRecord.Fitness)
            {
                return "Hypothecation";
            }
            else if (!vehicleRecord.Fitness)
            {
                return "Blacklist";
            }

            // Add more conditions for other status if needed

            return null; // Return null if no status has failed
        }

    }
}