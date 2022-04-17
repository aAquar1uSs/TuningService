using System.Data;
using System.Threading.Tasks;
using Npgsql;
namespace TuningService.Services.Impl
{
    public sealed class DbService : IDbService
    {
        private readonly NpgsqlConnection _sqlConnection;

        private readonly string connectionString;

        private const string SqlRequestForAllData = "SELECT customer.customer_id,"
                                                    + "concat(customer.surname,' ', customer.name, ' ', customer.lastname), customer.phone,"
                                                    + "car.car_id, concat(car.name, ' ', car.model), tuning_box.box_id,"
                                                    + "concat(master.name, ' ', master.surname), master.phone "
                                                    + "FROM customer JOIN car ON customer.customer_id = car.customer_id "
                                                    + "JOIN tuning_box ON car.car_id = tuning_box.car_id "
                                                    + "JOIN master ON tuning_box.master_id = master.master_id";

        public DbService(string connectionString)
        {
            _sqlConnection = new NpgsqlConnection(connectionString);
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

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                        dt.Load(reader);
                }
            }

            await _sqlConnection.CloseAsync();
            return dt;
        }
    }
}
