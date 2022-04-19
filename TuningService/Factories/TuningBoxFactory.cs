using Npgsql;
using TuningService.Models;

namespace TuningService.Factories;

public static class TuningBoxFactory
{
    public static TuningBox GetCommonDataInstance(NpgsqlDataReader reader)
    {
        var customerId = reader.GetInt32(0);
        var customerSurname = reader.GetString(1);
        var customerName= reader.GetString(2);
        var customerLastname= reader.GetString(3);
        var customerPhone = reader.GetString(4);

        var customer = new Customer(customerName,
            customerLastname, customerSurname, customerPhone)
        { Id = customerId };

        var carId = reader.GetInt32(5);
        var carName = reader.GetString(6);
        var carModel = reader.GetString(7);

        var car = new Car(carName, carModel) { Owner = customer, Id = carId};

        var tuningBoxId = reader.GetInt32(8);

        var masterId = reader.GetInt32(9);
        var masterName = reader.GetString(10);
        var masterSurname = reader.GetString(11);
        var masterPhone = reader.GetString(11);

        var master = new Master(masterName, masterSurname, masterPhone) { Id = masterId};

        return new TuningBox(master, car) { Id = tuningBoxId };
    }
}