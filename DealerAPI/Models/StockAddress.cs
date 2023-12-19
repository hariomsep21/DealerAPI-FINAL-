using System.ComponentModel.DataAnnotations.Schema;

namespace DealerAPI.Models
{
    public class StockAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string AddressType { get; set; }
        public int IdU { get; set; }
        [ForeignKey("IdU")]
        public virtual UserInfo User { get; set; }
    }
}
