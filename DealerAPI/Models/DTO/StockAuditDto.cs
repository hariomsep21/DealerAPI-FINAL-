using System.ComponentModel.DataAnnotations.Schema;

namespace Dealer.Model.DTO
{
    public class StockAuditDto
    {



        public int CarId { get; set; }


        public string CarName { get; set; }
        public string Variant { get; set; }
        public string DaysLeftToVerify { get; set; }


  

    }
}
