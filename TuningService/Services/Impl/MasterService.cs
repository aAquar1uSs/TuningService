using System.Data;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using TuningService.Factories;
using TuningService.Models;

namespace TuningService.Services.Impl;

public class MasterService : IMasterService
{
    private readonly NpgsqlConnection _sqlConnection;

    public MasterService(string sqlConnectionString)
    {
        _sqlConnection = new NpgsqlConnection(sqlConnectionString);
    }

    public async Task<Master> GetMasterByTuningBoxIdAsync(int tuningBoxId)
    {
        int? masterId = null;
        try
        {
            await _sqlConnection.OpenAsync();

            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText =
                    "SELECT tuning_box.master_id FROM tuning_box WHERE tuning_box.box_id = @box_id";

                command.Parameters.Add("@box_id", NpgsqlDbType.Integer).Value = tuningBoxId;

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        masterId = reader.GetInt32(0);
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

        return await GetMasterByIdAsync(masterId);
    }

    public async Task<Master> GetMasterByIdAsync(int? masterId)
    {
        if (masterId is null)
            return null;

        Master master = null;

        try
        {
            await _sqlConnection.OpenAsync();
            using var command = new NpgsqlCommand();

            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM master WHERE master.master_id = @master_id";

            command.Parameters.Add("@master_id", NpgsqlDbType.Integer).Value = masterId;

            await using (var reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    master = MasterFactory.GetMasterInstance(reader);
                }
            }

            await _sqlConnection.CloseAsync();
        }
        catch (NpgsqlException)
        {
            await _sqlConnection.CloseAsync();
            return null;
        }

        return master;
    }

    public async Task<DataTable> GetAllMastersAsync()
    {
        var dt = new DataTable();

        try
        {
            await _sqlConnection.OpenAsync();

            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT concat(name, ' ',  surname) FROM master";

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                }
            }
            await _sqlConnection.CloseAsync();
        }
        catch (NpgsqlException)
        {
            await _sqlConnection.CloseAsync();
            return dt;
        }

        return dt;
    }

    public async Task<int> GetMasterIdByFullInformation(Master master)
    {
        var masterId = 0;

        try
        {
            await _sqlConnection.OpenAsync();
            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT master_id FROM master "
                    + "WHERE name = @name AND surname = @surname";
                command.Parameters.Add("@name", NpgsqlDbType.Varchar).Value = master.Name;
                command.Parameters.Add("@surname", NpgsqlDbType.Varchar).Value = master.Surname;

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        masterId = reader.GetInt32(0);
                    }
                }
            }

            await _sqlConnection.CloseAsync();
        }
        catch (NpgsqlException)
        {
            await _sqlConnection.CloseAsync();
            return 0;
        }

        return masterId;
    }

    public async Task<bool> InsertNewMasterAsync(Master master)
    {
        try
        {
            await _sqlConnection.OpenAsync();
            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO master (name, surname, phone) "
                    + "VALUES (@name, @surname, @phone)";
                command.Parameters.Add("@name", NpgsqlDbType.Varchar).Value = master.Name;
                command.Parameters.Add("@surname", NpgsqlDbType.Varchar).Value = master.Surname;
                command.Parameters.Add("@phone", NpgsqlDbType.Varchar).Value = master.Phone;

                await using (_ = await command.ExecuteReaderAsync()) { }
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

    public async Task<bool> DeleteMasterByFullInfo(Master master)
    {
        try
        {
            await _sqlConnection.OpenAsync();
            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM master WHERE"
                    + " master.name = @name AND master.surname = @surname";
                command.Parameters.Add("@name", NpgsqlDbType.Varchar).Value = master.Name;
                command.Parameters.Add("@surname", NpgsqlDbType.Varchar).Value = master.Surname;

                await using (_ = await command.ExecuteReaderAsync()) { }
            }

            await _sqlConnection.CloseAsync();
            return true;
        }
        catch (NpgsqlException)
        {
            await _sqlConnection.CloseAsync();
            return false;
        }
    }
}
