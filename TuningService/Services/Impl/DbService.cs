using System.Data;
using System.Threading.Tasks;
using Npgsql;
using TuningService.Factories;
using TuningService.Models;

namespace TuningService.Services.Impl
{
    public sealed class DbService : IDbService
    {
        private readonly NpgsqlConnection _sqlConnection;

        private const string SqlRequestForAllData = "SELECT customer.customer_id,"
            + "concat(customer.surname,' ', customer.name, ' ', customer.lastname), customer.phone,"
            + "car.car_id, concat(car.name, ' ', car.model), tuning_box.box_id,"
            + "concat(master.name, ' ', master.surname), master.phone "
            + "FROM customer JOIN car ON customer.customer_id = car.customer_id "
            + "JOIN tuning_box ON car.car_id = tuning_box.car_id "
            + "JOIN master ON tuning_box.master_id = master.master_id";

        public DbService(NpgsqlConnection connection)
        {
            _sqlConnection = connection;
        }

        public async Task<DataTable> ShowAllDataAsync()
        {
            await _sqlConnection.OpenAsync();
            var dt = new DataTable();

            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = SqlRequestForAllData;

                var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                    dt.Load(reader);
            }

            await _sqlConnection.CloseAsync();
            return dt;
        }

        public async Task DeleteCustomerByIdAsync(int customerId)
        {
            await _sqlConnection.OpenAsync();
            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = $"DELETE FROM customer WHERE customer_id = {customerId}";
                _ = await command.ExecuteReaderAsync();
            }

            await _sqlConnection.CloseAsync();
        }

        public async Task<TuningBox> GetFulInformationAboutTuningBoxById(int tuningBoxId)
        {
            var order = await GetOrderByTuningBoxIdAsync(tuningBoxId);
            var master = await GetMasterByTuningBoxIdAsync(tuningBoxId);

            if (order is null || master is null)
                return null;

            return new TuningBox(tuningBoxId, master, order);
        }

        public async Task<Order> GetOrderByTuningBoxIdAsync(int tuningBoxId)
        {
            await _sqlConnection.OpenAsync();
            Order order = null;

            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT tuning_order.order_id, tuning_order.start_date, tuning_order.end_date,"
                    + " tuning_order.description, tuning_order.price, tuning_order.tuning_box_id, tuning_order.in_work " 
                    + " FROM tuning_order JOIN tuning_box ON"
                    + $" tuning_order.tuning_box_id = {tuningBoxId};";

                var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    order = OrderFactory.GetOrderInstance(reader);
                }
            }
            await _sqlConnection.CloseAsync();
            return order;
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
                    $"SELECT tuning_box.master_id FROM tuning_box WHERE tuning_box.box_id = {tuningBoxId}";

                var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    masterId = reader.GetInt32(0);
                }
            }
            await _sqlConnection.CloseAsync();

            return await GetMasterByIdAsync(masterId);
        }

        private async Task<Master> GetMasterByIdAsync(int? masterId)
        {
            if (masterId is null)
                return null;

            Master master = null;
            await _sqlConnection.OpenAsync();
            using var command = new NpgsqlCommand();

            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = $"SELECT * FROM master WHERE master.master_id = {masterId}";

            var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                await reader.ReadAsync();
                master = MasterFactory.GetMasterInstance(reader);
            }

            await _sqlConnection.CloseAsync();
            return master;
        }
    }
}
