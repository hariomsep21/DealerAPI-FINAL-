using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models
{
    public class AccountDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 10)]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 8)]
        public string IFSCCode { get; set; }

        [Required]
        [StringLength(55, MinimumLength = 10)]
        public string BankName { get; set; }


        [ForeignKey("UserInfo")]
        public int UserInfoId { get; set; }
        public virtual UserInfo UserInfos { get; set; }
    }
}
