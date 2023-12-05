using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models
{
    public class PV_NewCarDealer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? PurchaseAmount { get; set; }
        public string? VehicleNumber { get; set; }
        public string? OdometerPicture { get; set; }
        public string? VehiclePicFromFront { get; set; }
        public string? VehiclePicFromBack { get; set; }
        public string? Invoice { get; set; }
        public string? PictOfOrginalRC { get; set; }

        [ForeignKey("UserInfo")]
        public int UserInfoId { get; set; }
        public virtual UserInfo UserInfos { get; set; }
    }
}
