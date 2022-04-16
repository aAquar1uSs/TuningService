using Npgsql;
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

        return new Master(id, name, surname, phone);
    }
}