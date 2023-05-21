using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TuningService.Models;

namespace TuningService.Factories
{
    public static class OrderFactory
    {
        public static Order GetOrderInstance(DateTime finishDate, decimal price,
            bool inWork, string desc, TuningBox box = null)
        {
            var order = new Order
            {
                StartDate = DateTime.UtcNow,
                EndDate = finishDate,
                Description = desc,
                Price = price,
                IsDone = inWork,
                TuningBox = box
            };
            var results = new List<ValidationResult>();
            var context = new ValidationContext(order);


            if (!Validator.TryValidateObject(order, context, results, true))
            {
                throw new ValidationException();
            }

            return order;
        }
    }
}
