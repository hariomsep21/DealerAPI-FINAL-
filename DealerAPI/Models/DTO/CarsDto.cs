using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dealer.Model.DTO
{
    public class CarsDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Do not auto-generate
        public int CarId { get; set; }

        public string CarName { get; set; }
        public string Variant { get; set; }
        public string? Image { get; set; }

        // Navigation property for Payments related to this car
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Vehiclerecord> VehicleRecords { get; set; }
    }
}
