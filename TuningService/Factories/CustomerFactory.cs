using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TuningService.Models;

namespace TuningService.Factories;

public static class CustomerFactory
{
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
