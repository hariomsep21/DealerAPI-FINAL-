using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dealer.Model
{


    public enum AuditStatus
    {
        Failed,
        inprocess,
        sold
    }

    public class StockAudit
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }

        public string CarName => Car.CarName;
        public string Variant => Car.Variant;


        public DateTime AuditDate { get; set; }

        public AuditStatus? Status { get; set; }

        public string image1 { get; set; } = string.Empty;
        public string image2 { get; set; } = string.Empty;
        public string image3 { get; set; } = string.Empty;

        public bool varified {  get; set; }=false;

    }
}

