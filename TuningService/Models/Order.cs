using System;
using System.ComponentModel.DataAnnotations;

namespace TuningService.Models
{
    public sealed class Order
    {
        public int OrderId { get; set; }

        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool IsDone { get; set; }

        public TuningBox TuningBox { get; set; }
    }
}
