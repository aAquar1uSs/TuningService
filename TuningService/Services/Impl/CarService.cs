using System.Data;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using TuningService.Factories;
using TuningService.Models;

namespace TuningService.Services.Impl;

public class CarService : ICarService
{
    private readonly NpgsqlConnection _sqlConnection;

    public CarService(string connectionString)
    {
        _sqlConnection = new NpgsqlConnection(connectionString);
    }

    public async Task<Car> GetCarByTuningBoxIdAsync(int tuningBoxId)
    {
        int? carId = null;

        await _sqlConnection.OpenAsync();

        using (var command = new NpgsqlCommand())
        {
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText =
                "SELECT tuning_box.car_id FROM tuning_box WHERE tuning_box.box_id = @box_id";

            command.Parameters.Add("@box_id", NpgsqlDbType.Integer).Value = tuningBoxId;

            await using (var reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    carId = reader.GetInt32(0);
                }
            }
        }

        await _sqlConnection.CloseAsync();

        return await GetCarByIdAsync(carId);
    }

    public async Task<Car> GetCarByIdAsync(int? carId)
    {
        if (carId is null)
            return null;

        Car car = null;
        await _sqlConnection.OpenAsync();
        using var command = new NpgsqlCommand();

        command.Connection = _sqlConnection;
        command.CommandType = CommandType.Text;
        command.CommandText = "SELECT car_id, name, model FROM car WHERE car.car_id = @car_id";

        command.Parameters.Add("@car_id", NpgsqlDbType.Integer).Value = carId;

        await using (var reader = await command.ExecuteReaderAsync())
        {
            if (reader.HasRows)
            {
                await reader.ReadAsync();
                car = CarFactory.GetCarInstance(reader);
            }
        }

        await _sqlConnection.CloseAsync();
        return car;
    }

    public async Task InsertNewCarAsync(Car car)
    {
        await _sqlConnection.OpenAsync();

        using (var command = new NpgsqlCommand())
        {
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO car(name, model, customer_id)"
                + "VALUES(@name, @model, @ownerId);";
            command.Parameters.Add("@name", NpgsqlDbType.Varchar).Value = car.Name;
            command.Parameters.Add("@model", NpgsqlDbType.Varchar).Value = car.Model;
            command.Parameters.Add("@ownerId", NpgsqlDbType.Integer).Value = car.Owner.Id;

            await using (var reader = await command.ExecuteReaderAsync()) { };
        }

        await _sqlConnection.CloseAsync();
    }

    public async Task<int> GetCarIdByFullInformationAsync(Car car)
    {
        var carId = 0;

        await _sqlConnection.OpenAsync();
        using (var command = new NpgsqlCommand())
        {
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT car_id FROM car "
                + "WHERE name = @name AND model = @model AND customer_id = @ownerId";
            command.Parameters.Add("@name", NpgsqlDbType.Varchar).Value = car.Name;
            command.Parameters.Add("@model", NpgsqlDbType.Varchar).Value = car.Model;
            command.Parameters.Add("@ownerId", NpgsqlDbType.Integer).Value = car.Owner.Id;

            await using (var reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    carId = reader.GetInt32(0);
                }
            }
        }

        await _sqlConnection.CloseAsync();
        return carId;
    }

    public async Task UpdateCarDataAsync(Car car)
    {
        await _sqlConnection.OpenAsync();

        using (var command = new NpgsqlCommand())
        {
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE car"
                + " SET name = @name, model = @model WHERE car_id = @carId;";
            command.Parameters.Add("@name", NpgsqlDbType.Varchar).Value = car.Name;
            command.Parameters.Add("@model", NpgsqlDbType.Varchar).Value = car.Model;
            command.Parameters.Add("@carId", NpgsqlDbType.Integer).Value = car.Id;

            await using (var reader = await command.ExecuteReaderAsync()) { };
        }

        await _sqlConnection.CloseAsync();
    }
}