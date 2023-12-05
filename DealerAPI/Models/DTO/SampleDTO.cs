﻿using System.ComponentModel.DataAnnotations;

namespace Dealer.Model.DTO
{
    public class SampleDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
    }
}
