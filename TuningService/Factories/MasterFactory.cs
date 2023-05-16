using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TuningService.Models;

namespace TuningService.Factories;

public static class MasterFactory
{
    public static Master GetMasterInstance(string name, string surname, string phone = "+111111111111")
    {
        var master = new Master
        {
            Name = name,
            Surname = surname,
            Phone = phone
        };
        var results = new List<ValidationResult>();
        var context = new ValidationContext(master);

        if (!Validator.TryValidateObject(master, context, results, true))
        {
            throw new ValidationException();
        }

        return master;
    }
}