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

        await _sqlConnection.OpenAsync();

        using (var command = new NpgsqlCommand())
        {
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT tuning_order.order_id, tuning_order.start_date, tuning_order.end_date,"
                                  + " tuning_order.description, tuning_order.price, tuning_order.tuning_box_id, tuning_order.in_work "
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
        return order;
    }

}

