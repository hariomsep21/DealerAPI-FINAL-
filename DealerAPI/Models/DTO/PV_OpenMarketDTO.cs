using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models.DTO
{
    public class PV_OpenMarketDTO
    {
        public int UserInfoId { get; set; }
        public string? PurchaseAmount { get; set; }
        public string? TokenAmount { get; set; }
        public string? WithholdAmount { get; set; }

        [MaxLength(12)]
        public string? SellerContactNumber { get; set; }

        [EmailAddress]
        [RegularExpression(@"^[\w-]+@gmail\.(com|in)$", ErrorMessage = "Email must end with @gmail.com or @gmail.in")]
        public string? SellerEmailAddress { get; set; }
        public string? VehicleNumber { get; set; }
        public string? PaymentProof { get; set; }
        public string? SellerAdhaar { get; set; }
        public string? SellerPAN { get; set; }
        public string? PictureOfOriginalRC { get; set; }
        public string? OdometerPicture { get; set; }
        public string? VehiclePictureFromFront { get; set; }
        public string? VehiclePictureFromBack { get; set; }
    }
}
