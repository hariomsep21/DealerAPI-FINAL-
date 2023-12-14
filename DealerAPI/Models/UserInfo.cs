using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Dealer.Model;

namespace DealerAPI.Models
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }
        public int? OTP { get; set; }
        public string? UserName { get; set; }

        public string? UserEmail { get; set; }

        public string Phone { get; set; }
        public DateTime OTPExpiry { get; set; }
        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime? TokenCreated { get; set; } = DateTime.Now;
        public DateTime? TokenExpires { get; set; }


        public int? SId { get; set; }

        [ForeignKey("SId")]
        public virtual State State { get; set; }


        

       // public virtual UserPhone UserPhones { get; set; }

        public ICollection<ProfileInformation> profileInformations { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<PV_Aggregator> pV_Aggregators { get; set; }
        public ICollection<PV_NewCarDealer> pV_NewCarDealers { get; set; }
        public ICollection<PV_OpenMarket> pV_OpenMarkets { get; set; }
        public ICollection<AccountDetails> accountDetails { get; set; }
        public ICollection<Car> cars { get; set; }

    }
}