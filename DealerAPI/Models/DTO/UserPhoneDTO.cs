using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models.DTO
{
    public class UserPhoneDTO
    {
        [Key]
        public int PhoneId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
