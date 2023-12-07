using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Dealer.Model;

namespace DealerAPI.Models
{
    public class UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string UserName { get; set; } = string.Empty;

        [EmailAddress]
        [RegularExpression(@"^[\w-]+@gmail\.(com|in)$", ErrorMessage = "Email must end with @gmail.com or @gmail.in")]
        public string UserEmail { get; set; } = string.Empty;

        [ForeignKey("Status")]
        public int? StatusId { get; set; }

        [MaxLength(4)]
        public string? OTP { get; set; }

        [ForeignKey("State")]
        public int? StateId { get; set; }


        [ForeignKey("UserPhones")]
        public int? PhnId { get; set; }






        public virtual Status Statuss { get; set; }
        public virtual State State { get; set; }
        public virtual UserPhone UserPhones { get; set; }

        public ICollection<ProfileInformation> profileInformations { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<PV_Aggregator> pV_Aggregators { get; set; }
        public ICollection<PV_NewCarDealer> pV_NewCarDealers { get; set; }
        public ICollection<PV_OpenMarket> pV_OpenMarkets { get; set; }
        public ICollection<AccountDetails> accountDetails { get; set; }
        public ICollection<Car> cars { get; set; }

    }
}