using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using TuningService.Models;

namespace TuningService.Repository.Impl;

public class TuningBoxRepository : ITuningBoxRepository
{
    private readonly NpgsqlConnection _db;

    public TuningBoxRepository(NpgsqlConnection db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<TuningBox?> GetAsync(int tuningBoxId)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();
        
        var sqlQuery = "SELECT car.car_id AS CarId, car.brand, car.model, car.customer_id AS CustomerId, tb.box_number, m.master_id AS MasterId, m.name, "
                       + "m.surname, m.phone, cus.customer_id as CustomerId, cus.name, cus.surname, cus.lastname, cus.phone "
                       + "FROM tuning_box tb "
                       + "INNER JOIN car ON car.car_id = tb.car_id "
                       + "INNER JOIN master m ON m.master_id = tb.master_id "
                       + "INNER JOIN customer cus ON car.customer_id = cus.customer_id "
                       + "WHERE tb.box_id = @id;";
        
        var parameters = new { id = tuningBoxId };

        var results = await _db.QueryAsync<Car, int, Master, Customer, TuningBox>(
            sqlQuery,
            (car, boxNumber, master, customer) =>
            {
                if (car is not null)
                    car.Owner = customer;

                return new TuningBox
                {
                    BoxNumber = boxNumber,
                    Car = car,
                    Master = master,
                };
            },
            splitOn: "CarId,brand,model,CustomerId,box_number,MasterId,CustomerId",
            param: parameters,
            commandType: CommandType.Text);

        return results.FirstOrDefault();
    }

    public async Task<int> GetTuningBoxIdByCarIdAsync(int carId)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();
        
        var sqlQuery = "SELECT box_id FROM tuning_box WHERE car_id = @id";
        
        var parameters = new { id = carId };

        return await _db.QueryFirstOrDefaultAsync<int>(sqlQuery, parameters, commandType: CommandType.Text);
    }

    public async Task InsertAsync(TuningBox box)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();
        
        var sqlQuery = "INSERT INTO tuning_box(box_number, master_id, car_id) VALUES (@boxNum, @masterId, @carId)";

        var parameters = new { boxNum = box.BoxNumber, masterId = box.Master.MasterId, carId = box.Car.CarId };
        
        await _db.QueryAsync(sqlQuery, parameters, commandType: CommandType.Text);
    }

    public async Task UpdateMasterIdAsync(int oldId, int newId)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();
        
        var sqlQuery = "UPDATE tuning_box SET master_id = @newId WHERE master_id = @oldId;";
        
        var parameters = new { newId = newId, oldId = oldId };
        
        await _db.QueryAsync(sqlQuery, parameters, commandType: CommandType.Text);
    }
}