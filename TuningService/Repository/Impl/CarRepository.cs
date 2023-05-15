using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using TuningService.Models;

namespace TuningService.Repository.Impl;

public class CarRepository : ICarRepository
{
    private readonly NpgsqlConnection _db;

    public CarRepository(NpgsqlConnection db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<Car> GetAsync(int carId)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "SELECT car_id as id, name, model FROM car WHERE car.car_id = @carId";
        var parameters = new Dictionary<string, object>()
        {
            ["carId"] = carId
        };
        var car = await _db.QueryAsync<Car>(sqlQuery, parameters, commandType: CommandType.Text);
        
        return car.FirstOrDefault();
    }

    public async Task InsertAsync(Car car)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "INSERT INTO car(name, model, customer_id) VALUES(@name, @model, @owner);";
        var parameters = new Dictionary<string, object>()
        {
            ["name"] = car.Name,
            ["model"] = car.Name,
            ["owner"] = car.Owner.Id
        };
        
        await _db.QueryAsync(sqlQuery, parameters, commandType: CommandType.Text);
    }

    public async Task<int> GetCarIdByFullInformationAsync(Car car)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "SELECT car_id FROM car WHERE name = @name AND model = @model AND customer_id = @ownerId";
        var parameters = new Dictionary<string, object>
        {
            ["name"] = car.Name,
            ["model"] = car.Name,
            ["ownerId"] = car.Owner.Id
        };
        
        var carId = await _db.QueryAsync<int>(sqlQuery, parameters, commandType: CommandType.Text);

        return carId.FirstOrDefault();
    }

    public async Task UpdateAsync(Car car)
    { 
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "UPDATE car SET name = @name, model = @model WHERE car_id = @carId;";;
        var parameters = new Dictionary<string, object>
        {
            ["name"] = car.Name,
            ["model"] = car.Name,
            ["carId"] = car.Id
        };
        
        await _db.QueryAsync(sqlQuery, parameters, commandType: CommandType.Text);
    }
}