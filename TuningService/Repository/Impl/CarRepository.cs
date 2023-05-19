using System;
using System.Collections.Generic;
using System.Data;
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

        var sqlQuery = "SELECT car_id as id, brand, model FROM car WHERE car.car_id = @carId";
        var parameters = new { carId = carId };
        
        return await _db.QueryFirstOrDefaultAsync<Car>(sqlQuery, parameters, commandType: CommandType.Text);
    }

    public async Task<int> InsertAsync(Car car)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "INSERT INTO car(brand, model, customer_id) VALUES(@brand, @model, @owner) RETURNING car_id;";
        var parameters = new Dictionary<string, object>
        {
            ["brand"] = car.Brand,
            ["model"] = car.Brand,
            ["owner"] = car.Owner.CustomerId
        };
        
        return await _db.QuerySingleAsync<int>(sqlQuery, parameters);
    }

    public async Task UpdateAsync(Car car)
    { 
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "UPDATE car SET brand = @brand, model = @model WHERE car_id = @carId;";;
        var parameters = new Dictionary<string, object>
        {
            ["brand"] = car.Brand,
            ["model"] = car.Model,
            ["carId"] = car.CarId
        };
        
        await _db.QueryAsync(sqlQuery, parameters, commandType: CommandType.Text);
    }
}