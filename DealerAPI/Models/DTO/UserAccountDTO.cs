using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models.DTO
{
    public class UserAccountDTO
    {
        public int Id { get; set; }
        public string OTP {  get; set; }
        public string UserName { get; set; }
    }
}
