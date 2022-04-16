using System;


namespace TuningService.Models
{
    public sealed class Order
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool InWork { get; set; }

        public int TuningBoxId { get; set; }

        public Order(int id, DateTime startDate,
            DateTime endDate, string description,
            decimal price, int boxId, bool inWork)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            Price = price;
            TuningBoxId = boxId;
            InWork = inWork;
        }
    }
}
