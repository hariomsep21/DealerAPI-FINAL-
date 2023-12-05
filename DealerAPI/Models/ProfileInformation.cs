using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models
{
    public class ProfileInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 8)]
        public string ContactNumber { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(email\.com|gmail\.in)$", ErrorMessage = "Invalid email format.")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string ShopAddress { get; set; }

        [Required]
        [MaxLength(50)]
        public string ResidenceAddress { get; set; }

        [Required]
        [MaxLength(50)]
        public string AlternativeNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public int AccountDetails { get; set; }

        [ForeignKey("UserInfo")]
        public int UserInfoId { get; set; }
        public virtual UserInfo UserInfos { get; set; }


        // Navigation properties for the foreign keys


    }
}
