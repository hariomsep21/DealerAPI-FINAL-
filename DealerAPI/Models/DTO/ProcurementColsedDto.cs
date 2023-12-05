using System.Numerics;

namespace Dealer.Model.DTO
{
    public class ProcurementColsedDto:ProcDetailDto
    {
        public decimal? Amount_paid { get; set; }
        public DateTime? ColsedOn { get; set; }
    }
}
