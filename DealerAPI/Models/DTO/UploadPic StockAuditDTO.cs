using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models.DTO
{
    public class UploadPic_StockAuditDTO
    {
       
        public int Id { get; set; }
        public string image1 { get; set; } = string.Empty;
        public string image2 { get; set; } = string.Empty;
        public string image3 { get; set; } = string.Empty;

        public bool varified { get; set; } = false;
    }
}
