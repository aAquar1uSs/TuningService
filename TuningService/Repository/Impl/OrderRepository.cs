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

        var sqlQuery = "SELECT tuning_order.order_id as OrderId, tuning_order.start_date, tuning_order.end_date, "
                       + "tuning_order.description, tuning_order.price, tuning_order.is_done, "
                       + "tuning_box.box_id AS TuningBoxId, tuning_box.box_number "
                       + "FROM tuning_order "
                       + "JOIN tuning_box ON tuning_order.tuning_box_id = tuning_box.box_id "
                       + "WHERE tuning_box.box_id = @id";

        var parameters = new { id = tuningBoxId };

        var dynamicResults = await _db.QueryAsync<dynamic>(sqlQuery, parameters);

        var results = dynamicResults.Select(result =>
        {
            var tuningOrder = new Order
            {
                OrderId = result.orderid ?? 0,
                StartDate = ((DateTime)result.start_date).Date,
                EndDate = ((DateTime)result.end_date).Date,
                Description = result.description,
                Price = result.price,
                IsDone = result.is_done,
                TuningBox = new TuningBox
                {
                    BoxId = result.tuningboxid ?? 0,
                    BoxNumber = result.box_number
                }
            };

            return tuningOrder;
        });

        return results.FirstOrDefault();
    }

    public async Task ChangeStateAsync(Order order)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();
        
        var sqlQuery = "UPDATE tuning_order SET is_done = @isDone WHERE order_id = @id;";

        var parameters = new { id = order.OrderId, isDone = order.IsDone };

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
            ["boxId"] = order.TuningBox.BoxId
        };

        await _db.QueryAsync(sqlQuery, parameters, commandType: CommandType.Text);
    }

    public async Task<Order> GetAsync(int id)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();
        
        var sqlQuery = "SELECT tuning_order.order_id, tuning_order.start_date, tuning_order.end_date, " +
                       "tuning_order.description, tuning_order.price, tuning_order.is_done " +
                       "FROM tuning_order " +
                       "WHERE tuning_order.order_id = @orderId";

        var parameters = new { orderId = id };

        var result = await _db.QueryFirstOrDefaultAsync(sqlQuery, parameters, commandType: CommandType.Text);

        if (result == null) return null;
        
        var order = new Order
        {
            OrderId = result.order_id,
            StartDate = Convert.ToDateTime(result.start_date),
            EndDate = Convert.ToDateTime(result.end_date),
            Description = result.description, 
            Price = result.price,
            IsDone = result.is_done
        };

        return order;

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
            ["orderId"] = order.OrderId
        };

        await _db.QueryAsync(sqlQuery, parameters, commandType: CommandType.Text);
    }
}

