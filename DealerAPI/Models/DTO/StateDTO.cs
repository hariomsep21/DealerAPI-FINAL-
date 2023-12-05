using System.ComponentModel.DataAnnotations;

namespace DealerAPI.Models.DTO
{
    public class StateDTO
    {
        [Required]
        [MaxLength(50)]
        public string StateName { get; set; }
    }
}
