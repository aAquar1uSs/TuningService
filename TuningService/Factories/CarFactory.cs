using Npgsql;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using TuningService.Models;

namespace TuningService.Factories;

public static class CarFactory
{
    public static Result<Car> GetCarInstance(string name, string model, Customer customer = null)
    {
        var car = new Car(name, model) { Owner = customer };
        var results = new List<ValidationResult>();
        var context = new ValidationContext(car);

        return !Validator.TryValidateObject(car, context, results, true) 
            ? Result.Failure<Car>("Incorrect data entered!")
            : car;
    }
}