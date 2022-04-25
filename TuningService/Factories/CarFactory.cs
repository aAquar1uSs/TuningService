using Npgsql;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TuningService.Models;

namespace TuningService.Factories;

public static class CarFactory
{
    public static Car GetCarInstance(NpgsqlDataReader reader)
    {
        var id = reader.GetInt32(0);
        var name = reader.GetString(1);
        var model = reader.GetString(2);

        return new Car(name, model) { Id = id};
    }

    public static Car GetCarInstance(string name, string model, Customer customer = null)
    {
        var car = new Car(name, model) { Owner = customer };
        var results = new List<ValidationResult>();
        var context = new ValidationContext(car);

        if (!Validator.TryValidateObject(car, context, results, true))
        {
            throw new ValidationException();
        }

        return car;
    }
}