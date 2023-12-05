using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Dealer.Model.DTO
{
    public class ProcurementDto:ProcDetailDto
    {



        public int FilterId { get; set; }

        public decimal? Facility_Availed { get; set; }
        public decimal? Invoice_Charges { get; set; }
        public decimal? Amount_due { get; set; }
        public decimal? Amount_paid { get; set; }
        public decimal? Processing_charges { get; set; }
        // Car properties
        
       


   
    }
}
