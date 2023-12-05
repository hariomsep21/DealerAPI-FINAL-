using System.ComponentModel.DataAnnotations.Schema;

namespace Dealer.Model
{
    public class Vehiclerecord
    {
        public int Id { get; set; }
        public bool Challan { get; set; }
        public bool RcStatus { get; set; }
        public bool Fitness { get; set; }
        public bool OwnerName { get; set; }
        public string CarName => Car?.CarName;
        public string Variant => Car?.Variant;
        public int PurchaseId => Car.CarId;
        public bool Hypothecation { get; set; }
        public bool Blacklist { get; set; }
        // Foreign key referencing CarId
        public int CId { get; set; }
        [ForeignKey("CId")]
        // Navigation property for the related Car
        public virtual Car Car { get; set; }
        // Foreign key referencing CarId
    }
}
