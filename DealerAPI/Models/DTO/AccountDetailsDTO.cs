using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DealerAPI.Models.DTO
{
    public class AccountDetailsDTO
    {
        public int UserInfoId { get; set; }


        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 10)]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string BankName { get; set; }
    }
}
