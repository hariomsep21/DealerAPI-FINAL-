using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DealerAPI.Models
{
    public class State
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StateId { get; set; }

        [Required]
        [MaxLength(50)]
        public string StateName { get; set; }

        [Required]
        [MaxLength(14)]
        public string StateCode { get; set; }

        public ICollection<UserInfo> UserInfos { get; set; }
   
    }
}
