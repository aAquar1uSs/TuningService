using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TuningService.Models;

namespace TuningService.Factories
{
    public static class OrderFactory
    {
        public static Order GetOrderInstance(NpgsqlDataReader reader)
        {
            var id = reader.GetInt32(0);
            var startDate = reader.GetDateTime(1);
            var endDate = reader.GetDateTime(2);
            var description = reader.GetString(3);
            var price = reader.GetDecimal(4);
            var tuningBoxId = reader.GetInt32(5);
            var inWork = reader.GetBoolean(6);

            return new Order(startDate, endDate, description, price, inWork) { Id = id};
        }

        public static Order GetOrderInstance(DateTime finishDate, decimal price,
            bool inWork, string desc, TuningBox box)
        {
            var order = new Order(DateTime.UtcNow, finishDate, desc, price, inWork) { TuningBox = box };
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
