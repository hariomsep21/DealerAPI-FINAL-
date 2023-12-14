using DealerAPI.Data;
using DealerAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DealerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentProofImgController : ControllerBase
    {
        private readonly ILogger<PaymentProofImgController> _logger;
        private readonly ApplicationDbContext _db;
        public PaymentProofImgController(ILogger<PaymentProofImgController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost("PaymentPic")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UploadPic(PaymentProofImgDTO  proofImgDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingStockAudit = _db.Payment.FirstOrDefault(s => s.Id == proofImgDTO.Id);

                if (existingStockAudit == null)
                {
                    return NotFound($"Stock Audit with ID '{proofImgDTO.Id}' not found");
                }

                // Update the properties of the existing stock audit with the new values
                existingStockAudit.PaymentProofImg = proofImgDTO.PaymentProofImg;

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
