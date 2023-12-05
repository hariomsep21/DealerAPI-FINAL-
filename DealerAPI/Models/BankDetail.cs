using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dealer.Model
{
    public class BankDetail
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

        public ICollection<Payment> Payments { get; set; }
    }
}
