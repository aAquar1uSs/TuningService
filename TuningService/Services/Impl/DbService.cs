using Npgsql;
using System.Data;

namespace TuningService.Services
{
    public sealed class DbService : IDbService
    {
        private readonly NpgsqlConnection _sqlConnection;

        private const string SqlRequestForAllData = "SELECT customer.customer_id,"
            + "concat(customer.surname,' ', customer.name, ' ', customer.lastname), customer.phone,"
            + "car.car_id, concat(car.name, ' ', car.model), tuning_box.box_id, tuning_box.in_work,"
            + "concat(master.name, ' ', master.surname), master.phone "
            + "FROM customer JOIN car ON customer.customer_id = car.customer_id "
            + "JOIN tuning_box ON car.car_id = tuning_box.car_id "
            + "JOIN master ON tuning_box.master_id = master.master_id";

        public DbService(NpgsqlConnection connection)
        {
            _sqlConnection = connection;
        }

        public DataTable ShowAllData()
        {
            _sqlConnection.Open();
            var command = new NpgsqlCommand();
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = SqlRequestForAllData;

            DataTable dt = new DataTable();
            var reader = command.ExecuteReader();
            if (reader.HasRows)
                dt.Load(reader);

            command.Dispose();
            _sqlConnection.Close();

            return dt;
        }

        public void DeleteCustomerById(int customerId)
        {
            _sqlConnection.Open();
            var command = new NpgsqlCommand();
            command.Connection= _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = $"DELETE FROM customer WHERE customer_id = {customerId}";
            _ = command.ExecuteReader();

            command.Dispose();
            _sqlConnection.Close();
        }

        public DataTable ShowOrderByTuningBoxId(int tuningBoxId)
        {
            _sqlConnection.Open();
            var command = new NpgsqlCommand();
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM tuning_order JOIN tuning_box ON" 
                + $" tuning_order.tuning_box_id = {tuningBoxId};";

            DataTable dt = new DataTable();
            var reader = command.ExecuteReader();
            if (reader.HasRows)
                dt.Load(reader);

            command.Dispose();
            _sqlConnection.Close();

            return dt;
        }
    }
}
