using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models.DTO
{
    public class UserInfoDTO
    {

        public int Id { get; set; }
        public int? OTP { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string Phone { get; set; }
        public int? SId { get; set; }

    }
}
