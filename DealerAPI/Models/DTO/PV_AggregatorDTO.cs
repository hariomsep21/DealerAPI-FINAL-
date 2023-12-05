using System.ComponentModel.DataAnnotations.Schema;

namespace DealerAPI.Models.DTO
{
    public class PV_AggregatorDTO
    {
        public int UserInfoId { get; set; }
        public string PurchaseAmount { get; set; }

        public int MakeId { get; set; }

        public int ModelId { get; set; }
    
        public int YearOfRegistration { get; set; }
     
        public int VariantId { get; set; }
        
        public string PriceBreak { get; set; }
        public string StockIn { get; set; }
        public string RCAvailable { get; set; }


    }
}
