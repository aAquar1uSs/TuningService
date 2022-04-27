using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;
using System.Threading.Tasks;
using TuningService.Factories;
using TuningService.Models;

namespace TuningService.Services.Impl;

public class TuningBoxService : ITuningBoxService
{
    private readonly NpgsqlConnection _sqlConnection;

    public TuningBoxService(string sqlConnection)
    {
        _sqlConnection = new NpgsqlConnection(sqlConnection);
    }

    public async Task<TuningBox> GetFulInformationAboutTuningBoxById(int tuningBoxId)
    {
        TuningBox tuningBox = null;
        try
        {
            await _sqlConnection.OpenAsync();
            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT car.car_id, car.name, car.model, tb.box_number, m.master_id,m.name, "
                                      + "m.surname, m.phone, cus.customer_id, cus.name, cus.surname, cus.lastname, cus.phone "
                                      + "FROM tuning_box tb "
                                      + "INNER JOIN car ON car.car_id = tb.car_id "
                                      + "INNER JOIN master m on m.master_id = tb.master_id "
                                      + "INNER JOIN customer cus ON car.customer_id = cus.customer_id "
                                      + "WHERE tb.box_id = @id";

                command.Parameters.Add("@id", NpgsqlDbType.Integer).Value = tuningBoxId;

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();

                        tuningBox = TuningBoxFactory.GetTuningBoxInstance(reader);
                    }
                }
            }

            await _sqlConnection.CloseAsync();
        }
        catch (NpgsqlException)
        {
            await _sqlConnection.CloseAsync();
            return null;
        }
        return tuningBox;
    }

    public async Task<int> GetTuningBoxIdByCarIdAsync(int carId)
    {
        var boxId = 0;

        try
        {
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
        }
        catch (NpgsqlException)
        {
            await _sqlConnection.CloseAsync();
        }

        return boxId;
    }

    public async Task<bool> InsertNewTuningBoxAsync(TuningBox box)
    {
        try
        {
            await _sqlConnection.OpenAsync();
            using var command = new NpgsqlCommand();
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO tuning_box(box_number, master_id, car_id) VALUES (@boxNum, @masterId, @carId)";
            command.Parameters.Add("@boxNum", NpgsqlDbType.Integer).Value = box.BoxNumber;
            command.Parameters.Add("@masterId", NpgsqlDbType.Integer).Value = box.MasterInfo.Id;
            command.Parameters.Add("@carId", NpgsqlDbType.Integer).Value = box.CarInfo.Id;

            await using (_ = await command.ExecuteReaderAsync()) { };

            await _sqlConnection.CloseAsync();
        }
        catch (NpgsqlException)
        {
            await _sqlConnection.CloseAsync();
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateMasterIdAsync(int oldId, int newId)
    {
        try
        {
            await _sqlConnection.OpenAsync();
            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE tuning_box SET master_id = @newId WHERE master_id = @oldId;";
                command.Parameters.Add("@newId", NpgsqlDbType.Integer).Value = newId;
                command.Parameters.Add("@oldId", NpgsqlDbType.Integer).Value = oldId;

                await using (var reader = await command.ExecuteReaderAsync()) { };
            }
            await _sqlConnection.CloseAsync();
        }
        catch (NpgsqlException)
        {
            await _sqlConnection.CloseAsync();
            return false;
        }

        return true;
    }

    public async Task<bool> VerifyBoxNumberAsync(int boxNumber)
    {
        var isExist = false;
        try
        {
            await _sqlConnection.OpenAsync();
            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT EXISTS(SELECT box_number FROM tuning_box WHERE box_number = @number);";
                command.Parameters.Add("@number", NpgsqlDbType.Integer).Value = boxNumber;

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        isExist = reader.GetBoolean(0);
                    }
                }
            }
            await _sqlConnection.CloseAsync();
        }
        catch (NpgsqlException)
        {
            await _sqlConnection.CloseAsync();
        }
        return isExist;
    }
}