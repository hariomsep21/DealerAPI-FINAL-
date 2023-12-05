using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models
{
    public class Order_StockAudit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string Location {  get; set; }

        [ForeignKey("StockAudit_Purpose")]
        public int StockAudit_PurposeId { get; set; }
        public virtual StockAudit_Purpose? StockAudit_Purpose { get; set; }

        public DateTime ChooseDate { get; set; }

    }
}
