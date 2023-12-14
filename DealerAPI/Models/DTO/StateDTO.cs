using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models.DTO
{
    public class StateDTO
    {
        public int StateId { get; set; }
        [Required]
        [MaxLength(50)]
        public string StateName { get; set; }
    }
}
