using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models
{
    public class PVA_Variant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VariantId { get; set; }
        public string VariantName { get; set; }
        public int VariantCode { get; set; }

        public ICollection<PV_Aggregator> PV_Aggregators { get; set; }
    }
}
