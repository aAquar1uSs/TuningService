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

        return await GetMasterByIdAsync(masterId);
    }

    public async Task<Master> GetMasterByIdAsync(int? masterId)
    {
        if (masterId is null)
            return null;

        Master master = null;
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
        return master;
    }
}
