using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models.DTO
{
    public class CustomerSupportDTO
    {
        [Required]
        [MinLength(10)]
        public string Call { get; set; }

        [Required]
        [MinLength(10)]
        public string WhatsApp { get; set; }

        [Required]
        [EmailAddress] // Ensures that the input is a valid email address
        [RegularExpression(@"^[\w-]+@gmail\.(com|in)$", ErrorMessage = "Email must end with @gmail.com or @gmail.in")]
        public string Email { get; set; }
    }
}
