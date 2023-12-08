using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Dealer.Model.DTO
{
    public class PaymentPayDto: PaymentDto
    {
        public DateTime DueDate { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public decimal? AmountPaid { get; set; }
        public decimal? ProcessingCharges { get; set; }

        public string DaysLeft
        {
            get; set;

        }
        public string AccountNumber { get; set; }


        public string IFSCCode { get; set; }

        public string BankName { get; set; }
        public string Name { get; set; }
      
    }
}
