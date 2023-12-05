using System.ComponentModel.DataAnnotations;

namespace Dealer.Model.DTO
{
    public class BankDetailDto
    {
        [Key]
        public int RepaymentDetailId { get; set; }
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string AccountNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string IFSCCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string BankName { get; set; }

        // Foreign key referencing Payment table



        public virtual Payment Payment { get; set; }
    }
}
