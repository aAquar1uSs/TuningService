using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using TuningService.Models;
using TuningService.Models.ViewModels;

namespace TuningService.Repository.Impl;

public class MasterRepository : IMasterRepository
{
    private readonly NpgsqlConnection _db;

    public MasterRepository(NpgsqlConnection db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }
    
    public async Task<IReadOnlyCollection<MasterViewModel>> GetAllAsync()
    {
        if (_db.State == ConnectionState.Closed)
           _db.Open();

        var sqlQuery = "SELECT concat(name, ' ',  surname) AS MasterInfo FROM master";;

        var result = await _db.QueryAsync<MasterViewModel>(sqlQuery, commandType: CommandType.Text);

        return result.ToArray();
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
        
        return await _db.QueryFirstOrDefaultAsync<int>(sqlQuery, parameters, commandType: CommandType.Text);
    }

    public async Task<int> InsertAsync(Master master)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "INSERT INTO master (name, surname, phone) VALUES (@name, @surname, @phone) RETURNING master_id";
        var parameters = new Dictionary<string, object>
        {
            ["name"] = master.Name,
            ["surname"] = master.Surname,
            ["phone"] = master.Phone
        };

       return await _db.QueryFirstOrDefaultAsync<int>(sqlQuery, parameters, commandType: CommandType.Text);
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
