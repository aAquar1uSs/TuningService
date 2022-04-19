using Npgsql;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TuningService.Models;

namespace TuningService.Factories;

public static class MasterFactory
{
    public static Master GetMasterInstance(NpgsqlDataReader reader)
    {
        var id = reader.GetInt32(0);
        var name = reader.GetString(1);
        var surname = reader.GetString(2);
        var phone = reader.GetString(3);

        return new Master(name, surname, phone) { Id = id};
    }

    public static Master GetMasterInstance(string name, string surname, string phone = "+1111111111111")
    {
        var master = new Master(name, surname, phone);
        var results = new List<ValidationResult>();
        var context = new ValidationContext(master);

        if (!Validator.TryValidateObject(master, context, results, true))
        {
            throw new ValidationException();
        }

        return master;
    }
}