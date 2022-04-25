using Npgsql;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TuningService.Models;

namespace TuningService.Factories;

public static class CustomerFactory
{
    public static Customer GetCustomerInstance(NpgsqlDataReader reader)
    {
        var id = reader.GetInt32(0);
        var name = reader.GetString(1);
        var lastname = reader.GetString(2);
        var surname = reader.GetString(3);
        var phone = reader.GetString(4);

        return new Customer(name, lastname, surname, phone) { Id = id};
    }

    public static Customer GetCustomerInstance(string customerName, string customerLastname,
        string customerSurname, string customerPhone)
    {
        var customer = new Customer(customerName, customerLastname, customerSurname, customerPhone);
        var results = new List<ValidationResult>();
        var context = new ValidationContext(customer);

        if (!Validator.TryValidateObject(customer, context, results, true))
        {
            throw new ValidationException();
        }

        return customer;
    }
}
