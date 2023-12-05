using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dealer.Model
{
    
    public class ProcDetails
    {
        [Key]
        public int Id { get; set; }

        public DateTime? ClosedOn { get; set; }

        public int CarId => Payment.CarId;

        public decimal Due_Amount => Payment.Amount_Due;
        public decimal? Paid_Amount => Payment.AmountPaid;
        public decimal? ProcessingCharges => Payment.ProcessingCharges;
        public decimal? Facility_Availed => Payment.Facility_Availed;
        public decimal? Invoice_Charges => Payment.Invoice_Charges;
        public int Status { get; set; }
        

        public int FilterId { get; set; }

        [ForeignKey("FilterId")]
        public virtual ProcurementFilter ProcurementFilter { get; set; }
        public string Purchased_Amount { get; set; }
        public int PayId { get; set; }
        [ForeignKey("PayId")]
        public virtual Payment Payment { get; set; }
        //[ForeignKey("Car")]
        //public int CarId { get; set; }
        //public Car Car { get; set; }

    }
}
