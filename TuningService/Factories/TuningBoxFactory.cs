using Npgsql;
using TuningService.Models;

namespace TuningService.Factories;

public static class TuningBoxFactory
{
    public static TuningBox GetTuningBoxInstance(NpgsqlDataReader reader)
    {
        var car = new Car(reader.GetString(1), reader.GetString(2))
        {
            Id = reader.GetInt32(0),
            Owner = new Customer(reader.GetString(9),
                           reader.GetString(11),
                           reader.GetString(10),
                           reader.GetString(12))
            {
                Id = reader.GetInt32(8)
            }
        };

        var master = new Master(reader.GetString(5), reader.GetString(6),
            reader.GetString(7))
        { 
            Id = reader.GetInt32(4) 
        };

        return new TuningBox(reader.GetInt32(3), master, car);
    }

    public static TuningBox GetTuningBoxInstance(int boxNumber, Master master, Car car)
    {
        return new TuningBox(boxNumber, master, car);
    }
}