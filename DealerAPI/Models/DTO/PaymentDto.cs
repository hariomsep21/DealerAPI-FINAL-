using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Dealer.Model.DTO
{
    public class PaymentDto
    {
      public int Id { get; set; }   
        public decimal? Amount_Due { get; set; }

        public int CarId { get; set; } // Foreign key referencing CarId

        // Navigation property for the related Car
        

        // CarName, Variant, and Image properties from Car
        public string CarName {  get; set; }
        public string Variant { get; set; }
       

    }
}
