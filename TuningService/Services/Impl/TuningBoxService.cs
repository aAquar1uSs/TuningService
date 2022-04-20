using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Services.Impl;

public class TuningBoxService : ITuningBoxService
{
    private readonly ICarService _carService;

    private readonly ICustomerService _customerService;

    private readonly IMasterService _masterService;

    private readonly NpgsqlConnection _sqlConnection;

    public TuningBoxService(string sqlConnection,ICarService carService,
        IMasterService masterService,
        ICustomerService customerService)
    {
        _sqlConnection = new NpgsqlConnection(sqlConnection);
        _carService = carService;
        _masterService = masterService;
        _customerService = customerService;
    }

    public async Task<TuningBox> GetFulInformationAboutTuningBoxById(int tuningBoxId)
    {
        var car = await _carService.GetCarByTuningBoxIdAsync(tuningBoxId);
        car.Owner = await _customerService.GetCustomerByCarIdAsync(car.Id);
        var master = await _masterService.GetMasterByTuningBoxIdAsync(tuningBoxId);

        if (car is null || master is null)
            return null;

        return new TuningBox(master, car) { Id = tuningBoxId};
    }

    public async Task<int> GetTuningBoxIdByCarIdAsync(int carId)
    {
        var boxId = 0;

        await _sqlConnection.OpenAsync();
        using (var command = new NpgsqlCommand())
        {
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT box_id FROM tuning_box WHERE car_id = @id";
            command.Parameters.Add("@id", NpgsqlDbType.Integer).Value = carId;

            await using (var reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    boxId = reader.GetInt32(0);
                }
            }
        }

        await _sqlConnection.CloseAsync();

        return boxId;
    }

    public async Task InsertNewTuningBox(int carId, int masterId)
    {
        await _sqlConnection.OpenAsync();
        using (var command = new NpgsqlCommand())
        {
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO tuning_box(master_id, car_id) VALUES (@masterId, @carId)";
            command.Parameters.Add("@masterId", NpgsqlDbType.Integer).Value = masterId;
            command.Parameters.Add("@carId", NpgsqlDbType.Integer).Value = carId;

            await using (var reader = await command.ExecuteReaderAsync()) { };
        }

        await _sqlConnection.CloseAsync();
    }
}