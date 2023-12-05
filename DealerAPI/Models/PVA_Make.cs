using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models
{
    public class PVA_Make
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MakeId { get; set; }
        public string MakeName { get; set; }
        public int MakeCode { get; set; }

       public ICollection<PV_Aggregator> PV_Aggregators { get; set;}
    }
}
