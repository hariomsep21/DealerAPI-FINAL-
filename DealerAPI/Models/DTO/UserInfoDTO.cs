using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models.DTO
{
    public class UserInfoDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string UserName { get; set; }

        [EmailAddress]
        [RegularExpression(@"^[\w-]+@gmail\.(com|in)$", ErrorMessage = "Email must end with @gmail.com or @gmail.in")]
        public string UserEmail { get; set; }

     public string otp {  get; set; }
        public int StateId { get; set; }

        public int PhnId { get; set; }

      
        public int StatusId { get; set; }

     
    }
}
