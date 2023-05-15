using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using TuningService.Models;

namespace TuningService.Repository.Impl;

public class MasterRepository : IMasterRepository
{
    private readonly NpgsqlConnection _db;

    public MasterRepository(NpgsqlConnection db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    //ToDo refactor it
    public async Task<DataTable> GetAllAsync()
    {
        var dt = new DataTable();

        try
        {
            await _db.OpenAsync();

            using (var command = new NpgsqlCommand())
            {
                command.Connection = _db;
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
            await _db.CloseAsync();
        }
        catch (NpgsqlException)
        {
            await _db.CloseAsync();
            return dt;
        }

        return dt;
    }

    public async Task<int> GetMasterIdAsync(Master master)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "SELECT master_id FROM master WHERE name = @name AND surname = @surname";;
        var parameters = new Dictionary<string, object>
        {
            ["name"] = master.Name,
            ["surname"] = master.Surname,
        };
        
        return (await _db.QueryAsync<int>(sqlQuery, parameters, commandType: CommandType.Text)).FirstOrDefault();
    }

    public async Task InsertAsync(Master master)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "INSERT INTO master (name, surname, phone) VALUES (@name, @surname, @phone)";
        var parameters = new Dictionary<string, object>
        {
            ["name"] = master.Name,
            ["surname"] = master.Surname,
            ["phone"] = master.Phone
        };

        await _db.QueryAsync(sqlQuery, parameters, commandType: CommandType.Text);
    }

    public async Task DeleteAsync(Master master)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "DELETE FROM master WHERE master.name = @name AND master.surname = @surname";
        var parameters = new Dictionary<string, object>
        {
            ["name"] = master.Name,
            ["surname"] = master.Surname
        };

        await _db.QueryAsync(sqlQuery, parameters, commandType: CommandType.Text);
    }
}
