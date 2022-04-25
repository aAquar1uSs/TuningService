using System;
using System.ComponentModel.DataAnnotations;

namespace TuningService.Models
{
    public sealed class Order
    {
        public int Id { get; set; }

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

        public Order(DateTime startDate,
            DateTime endDate, string description,
            decimal price, bool inWork)
        {
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            Price = price;
            IsDone = inWork;
        }
    }
}
