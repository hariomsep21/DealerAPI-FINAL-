using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models
{
    public class LastUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActiveId { get; set; }
        public int UserValue { get; set; }
    }
}
