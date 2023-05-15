using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using TuningService.Models;
namespace TuningService.Repository.Impl;

public class OrderRepository : IOrderRepository
{
    private readonly NpgsqlConnection _db;

    public OrderRepository(NpgsqlConnection db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<Order?> GetOrderByTuningBoxIdAsync(int tuningBoxId)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "SELECT tuning_order.order_id, tuning_order.start_date, tuning_order.end_date,"
                       + " tuning_order.description, tuning_order.price, tuning_order.is_done "
                       + " FROM tuning_order JOIN tuning_box ON"
                       + " tuning_order.tuning_box_id = @box_id";
        var parameters = new Dictionary<string, object>
        {
            ["box_id"] = tuningBoxId
        };

        return (await _db.QueryAsync<Order>(sqlQuery, parameters, commandType: CommandType.Text)).FirstOrDefault();
    }

    public async Task ChangeStateAsync(Order order)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();
        
        var sqlQuery = "UPDATE tuning_order SET is_done = @isDone WHERE order_id = @id;";
        var parameters = new Dictionary<string, object>
        {
            ["id"] = order.Id,
            ["isDone"] = order.IsDone
        };

        await _db.QueryAsync(sqlQuery, parameters, commandType: CommandType.Text);
    }

    public async Task InsertAsync(Order order)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();
        
        var sqlQuery = "INSERT INTO tuning_order(end_date, description, price, is_done, tuning_box_id) "
                       + "VALUES (@endDate, @desc, @price, @isDone, @boxId)";
        var parameters = new Dictionary<string, object>
        {
            ["endDate"] = order.EndDate,
            ["desc"] = order.Description,
            ["price"] = order.Price,
            ["isDone"] = order.IsDone,
            ["boxId"] = order.TuningBox.Id
        };

        await _db.QueryAsync(sqlQuery, parameters, commandType: CommandType.Text);
    }

    public async Task<Order> GetAsync(int id)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();
        
        var sqlQuery = "SELECT tuning_order.order_id, tuning_order.start_date, tuning_order.end_date,"
                       + " tuning_order.description, tuning_order.price, tuning_order.is_done FROM tuning_order "
                       + " WHERE tuning_order.order_id = @orderId";
        var parameters = new Dictionary<string, object>
        {
            ["orderId"] = id,
        };

        return (await _db.QueryAsync<Order>(sqlQuery, parameters, commandType: CommandType.Text)).FirstOrDefault();
    }

    public async Task UpdateAsync(Order order)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();
        
        var sqlQuery = "UPDATE tuning_order"
                       + " SET end_date = @endDate, description = @desc, price = @price, is_done = @isDone"
                       + " WHERE tuning_order.order_id = @orderId;";
        var parameters = new Dictionary<string, object>
        {
            ["endDate"] = order.EndDate,
            ["desc"] = order.Description,
            ["price"] = order.Price,
            ["isDone"] = order.IsDone,
            ["orderId"] = order.Id
        };

        await _db.QueryAsync(sqlQuery, parameters, commandType: CommandType.Text);
    }
}

