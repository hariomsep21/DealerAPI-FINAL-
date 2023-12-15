using DealerAPI.Data;
using DealerAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        public IActionResult UploadPic([FromBody] PaymentProofImgDTO proofImgDTO, int Id)
        {
            try
            {
                if (proofImgDTO == null || !ModelState.IsValid || proofImgDTO.PaymentProofImg == null)
                {
                    return BadRequest(ModelState);
                }

                var existingPayment = _db.Payment.FirstOrDefault(s => s.Id == Id);

                if (existingPayment == null)
                {
                    return NotFound($"Payment not found");
                }

                // Generate a unique identifier for the image file
                string uniqueIdentifier = Guid.NewGuid().ToString();

                // Get the directory where your API project is located
                string apiProjectDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Construct the relative path within the API project
                string imagePathRelative = Path.Combine("Images", $"{uniqueIdentifier}.jpg"); // Update the extension as needed

                // Construct the absolute path by combining the API project directory with the relative path
                string imagePathAbsolute = Path.Combine(apiProjectDirectory, imagePathRelative);

                // Ensure the directory exists, create it if not
                Directory.CreateDirectory(Path.GetDirectoryName(imagePathAbsolute));



                // Update the properties of the existing payment with the image path
                existingPayment.PaymentProofImg = imagePathRelative; // Assuming you have this property in your Payment model

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


