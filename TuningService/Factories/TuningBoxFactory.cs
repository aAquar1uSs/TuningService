using Npgsql;
using TuningService.Models;

namespace TuningService.Factories;

public static class TuningBoxeFactory
{
    public static CommonData GetCommonDataInstance(NpgsqlDataReader reader)
    {
        var customerId = reader.GetInt32(0);
        var customerSurname = reader.GetString(1);
        var customerName= reader.GetString(2);
        var customerLastname= reader.GetString(3);
        var customerPhone = reader.GetString(4);

        var customer = new Customer(customerId, customerName, 
            customerLastname, customerSurname, customerPhone);

        var carId = reader.GetInt32(5);
        var carName = reader.GetString(6);
        var carModel = reader.GetString(7);

        var car = new Car(carId, carName, carModel, customerId) {Owner = customer};
        
        

    }
}