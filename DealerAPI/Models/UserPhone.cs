using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models
{
    public class UserPhone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhoneId { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<UserInfo> UserInfos { get; set; }
    }
}
