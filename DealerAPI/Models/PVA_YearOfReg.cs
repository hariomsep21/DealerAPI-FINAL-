using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models
{
    public class PVA_YearOfReg
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int YearId { get; set; }
        public int YearCode { get; set; }

        public ICollection<PV_Aggregator> PV_Aggregators { get; set; }
    }
}
