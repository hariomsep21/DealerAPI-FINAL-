using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models
{
    public class StockAudit_Purpose
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PurposeName { get; set; }

        public ICollection<Order_StockAudit> Order_StockAudits { get; set; }    
    }
}
