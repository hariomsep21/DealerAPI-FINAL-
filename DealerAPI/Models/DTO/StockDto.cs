namespace Dealer.Model.DTO
{
    public class StockDto
    {
        public int CarId { get; set; }


        public string CarName { get; set; }
        public string Variant { get; set; }
        public DateTime AuditDate { get; set; }
        public AuditStatus? Status { get; set; }
    }
}
