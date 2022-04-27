using System.Data;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using TuningService.Factories;
using TuningService.Models;

namespace TuningService.Services.Impl;

public class OrderService : IOrderService
{

    private readonly NpgsqlConnection _sqlConnection;

    public OrderService(string sqlConnectionString)
    {
        _sqlConnection = new NpgsqlConnection(sqlConnectionString);
    }

    public async Task<Order> GetOrderByTuningBoxIdAsync(int tuningBoxId)
    {
        Order order = null;

        try
        {
            await _sqlConnection.OpenAsync();

            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT tuning_order.order_id, tuning_order.start_date, tuning_order.end_date,"
                                      + " tuning_order.description, tuning_order.price, tuning_order.is_done "
                                      + " FROM tuning_order JOIN tuning_box ON"
                                      + " tuning_order.tuning_box_id = @box_id";

                command.Parameters.Add("@box_id", NpgsqlDbType.Integer).Value = tuningBoxId;

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        order = OrderFactory.GetOrderInstance(reader);
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

        return order;
    }

    public async Task ChangeStateOrderByInstance(Order order)
    {
        try
        {
            await _sqlConnection.OpenAsync();

            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE tuning_order "
                                      + "SET is_done = @isDone WHERE order_id = @id;";

                command.Parameters.Add("@id", NpgsqlDbType.Integer).Value = order.Id;
                command.Parameters.Add("@isDone", NpgsqlDbType.Boolean).Value = !order.IsDone;
                order.IsDone = !order.IsDone;
                await using (_ = await command.ExecuteReaderAsync())
                {
                }
            }
            await _sqlConnection.CloseAsync();
        }
        catch (NpgsqlException)
        {
            await _sqlConnection.CloseAsync();
        }
    }

    public async Task InsertNewOrderAsync(Order order)
    {
        try
        {
            await _sqlConnection.OpenAsync();
            using var command = new NpgsqlCommand();
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO tuning_order(end_date, description, price, is_done, tuning_box_id) "
                                  + "VALUES (@endDate, @desc, @price, @isDone, @boxId)";
            command.Parameters.Add("@endDate", NpgsqlDbType.Date).Value = order.EndDate;
            command.Parameters.Add("@desc", NpgsqlDbType.Text).Value = order.Description;
            command.Parameters.Add("@price", NpgsqlDbType.Money).Value = order.Price;
            command.Parameters.Add("@isDone", NpgsqlDbType.Boolean).Value = order.IsDone;
            command.Parameters.Add("@boxId", NpgsqlDbType.Integer).Value = order.TuningBox.Id;

            await using (_ = await command.ExecuteReaderAsync())
            {
            }

            await _sqlConnection.CloseAsync();
        }
        catch (NpgsqlException)
        {
            await _sqlConnection.CloseAsync();
        }

    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        Order order = null;
        try
        {
            await _sqlConnection.OpenAsync();

            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT tuning_order.order_id, tuning_order.start_date, tuning_order.end_date,"
                                      + " tuning_order.description, tuning_order.price, tuning_order.is_done FROM tuning_order "
                                      + " WHERE tuning_order.order_id = @orderId";

                command.Parameters.Add("@orderId", NpgsqlDbType.Integer).Value = id;

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        order = OrderFactory.GetOrderInstance(reader);
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

        return order;
    }

    public async Task<bool> UpdateOrderDataByFullInfo(Order order)
    {
        try
        {
            await _sqlConnection.OpenAsync();

            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE tuning_order"
                                      + " SET end_date = @endDate, description = @desc, price = @price, is_done = @isDone"
                                      + " WHERE tuning_order.order_id = @orderId;";
                command.Parameters.Add("@endDate", NpgsqlDbType.Date).Value = order.EndDate;
                command.Parameters.Add("@desc", NpgsqlDbType.Text).Value = order.Description;
                command.Parameters.Add("@price", NpgsqlDbType.Money).Value = order.Price;
                command.Parameters.Add("@isDone", NpgsqlDbType.Boolean).Value = order.IsDone;
                command.Parameters.Add("@orderId", NpgsqlDbType.Integer).Value = order.Id;

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
}

