using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models
{
    public class PVA_Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int ModelCode { get; set; }

        public ICollection<PV_Aggregator> PV_Aggregators { get; set; }
    }
}
