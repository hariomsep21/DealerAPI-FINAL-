using Dealer.Model.DTO;
using DealerAPI.Data;
using DealerAPI.Models;
using DealerAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadPic_StockAuditController : ControllerBase
    {
        private readonly ILogger<UploadPic_StockAuditController> _logger;
        private readonly ApplicationDbContext _db;
        public UploadPic_StockAuditController(ILogger<UploadPic_StockAuditController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost("uploadingPic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UploadPic(UploadPic_StockAuditDTO uploadPic_StockAuditDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingStockAudit = _db.StockAudits.FirstOrDefault(s => s.Id == uploadPic_StockAuditDTO.Id);

                if (existingStockAudit == null)
                {
                    return NotFound($"Stock Audit with ID '{uploadPic_StockAuditDTO.Id}' not found");
                }

                // Update the properties of the existing stock audit with the new values
                existingStockAudit.image1 = uploadPic_StockAuditDTO.image1;
                existingStockAudit.image2 = uploadPic_StockAuditDTO.image2;
                existingStockAudit.image3 = uploadPic_StockAuditDTO.image3;

                // Set varified based on the condition
                existingStockAudit.varified = !string.IsNullOrEmpty(uploadPic_StockAuditDTO.image1)
                    && !string.IsNullOrEmpty(uploadPic_StockAuditDTO.image2)
                    && !string.IsNullOrEmpty(uploadPic_StockAuditDTO.image3);

                // Save changes to the database
                _db.SaveChanges();

                return Ok("Upload successful");
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "Error occurred while processing the request");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }

        }

    }
}
