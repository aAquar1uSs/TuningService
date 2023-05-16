using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;

namespace TuningService.Repository.Impl
{
    public sealed class CommonService : ICommonService
    {
        private readonly NpgsqlConnection _db;
        
        private const string SqlRequestSearchInfo = "SELECT customer.customer_id,"
                                                        + "concat(customer.surname,' ', customer.name, ' ', customer.lastname), customer.phone,"
                                                        + "car.car_id, concat(car.name, ' ', car.model), tuning_box.box_id,"
                                                        + "concat(master.name, ' ', master.surname), master.phone "
                                                        + "FROM customer JOIN car ON customer.customer_id = car.customer_id "
                                                        + "JOIN tuning_box ON car.car_id = tuning_box.car_id "
                                                        + "JOIN master ON tuning_box.master_id = master.master_id "
                                                        + "WHERE customer.customer_id = @customerId or customer.name = @name "
                                                        + "or customer.surname = @surname or customer.lastname = @lastname "
                                                        + "or customer.phone = @phone";

        public CommonService(NpgsqlConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<DataTable> ShowAllDataAsync()
        {
            var dt = new DataTable();

            try
            {
                await _db.OpenAsync();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = _db;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT customer.customer_id,"
                                          + "concat(customer.surname,' ', customer.name, ' ', customer.lastname), customer.phone,"
                                          + "car.car_id, concat(car.brand, ' ', car.model), tuning_box.box_id,"
                                          + "concat(master.name, ' ', master.surname), master.phone "
                                          + "FROM customer JOIN car ON customer.customer_id = car.customer_id "
                                          + "JOIN tuning_box ON car.car_id = tuning_box.car_id "
                                          + "JOIN master ON tuning_box.master_id = master.master_id";

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                            dt.Load(reader);
                    }
                }

                await _db.CloseAsync();
            }
            catch (NpgsqlException)
            {
                await _db.CloseAsync();
            }

            return dt;
        }
        
        //ToDo add new model for view this data
        public async Task<DataTable> SearchCustomerByValueAsync(string value)
        {
            var dataTable = new DataTable();

            var customerId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            var name = value;
            var surname = value;
            var lastname = value;
            var phone = value;

            /*try
            {
                await _db.OpenAsync();
    
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = _db;
                    command.CommandType = CommandType.Text;
                    command.CommandText = SqlRequestSearchCustomer;
    
                    command.Parameters.Add("@customerId", NpgsqlDbType.Integer).Value = customerId;
                    command.Parameters.Add("@name", NpgsqlDbType.Varchar).Value = name;
                    command.Parameters.Add("@surname", NpgsqlDbType.Varchar).Value = surname;
                    command.Parameters.Add("@lastname", NpgsqlDbType.Varchar).Value = lastname;
                    command.Parameters.Add("@phone", NpgsqlDbType.Varchar).Value = phone;
    
                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
    
                await _db.CloseAsync();
            }
            catch (NpgsqlException)
            {
                await _db.CloseAsync();
                return dataTable;
            }*/
        
            if (_db.State == ConnectionState.Closed)
                _db.Open();
        
            var parameters = new Dictionary<string, object>
            {
                ["customerId"] = customerId,
                ["name"] = name,
                ["surname"] = surname,
                ["lastname"] = lastname,
                ["phone"] = phone
            };
        

            return dataTable;
        }

        public async Task Insert(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                var rowsArray = row.ItemArray.Select(x => x.ToString()).ToArray();
                var name = rowsArray[0];
                var surname = rowsArray[1];
                var lastname = rowsArray[2];
                var phone = rowsArray[3];
                var car = rowsArray[4];
                var carModel = rowsArray[5];
                var tuningBox = rowsArray[6];
                var startDate = DateTime.Parse(rowsArray[7]);
                var endDate = DateTime.Parse(rowsArray[8]);
                var description = rowsArray[9];
                var price = Decimal.Parse(rowsArray[10]);

                await _db.OpenAsync();
                using var command = new NpgsqlCommand();
                command.Connection = _db;
                command.CommandType = CommandType.Text;
                command.CommandText = "with first_customer_insert as ( " +
                    "insert into customer(name,lastname,surname,phone) " + 
                    "values(@name,@lastname,@surname,@phone)" + 
                    "RETURNING customer_id ), " + 
                "second_car_insert as ( " +
                "insert into car(name ,model,customer_id) " +
                "values " +
                "(@car,@carModel,(select customer_id from first_customer_insert)) " +
                "RETURNING car_id), " +
                "third_tuning_box_insert as ( " +
                "insert into tuning_box(box_number,master_id,car_id) " +
                "values (@tuningBox,(select master_id from master limit 1),(select car_id from second_car_insert)) " +
                "RETURNING tuning_box_id), " +
                "insert into tuning_order(start_date,end_date,description,price,is_done,tuning_box_id) " + 
                "values " +
                "(@startDate,@endDate,@description,@price,@isDone,(select tuning_box_id from third_tuning_box_insert));";
                
                command.Parameters.Add("@name", NpgsqlDbType.Varchar).Value = name;
                command.Parameters.Add("@surname", NpgsqlDbType.Varchar).Value = surname;
                command.Parameters.Add("@lastname", NpgsqlDbType.Varchar).Value = lastname;
                command.Parameters.Add("@phone", NpgsqlDbType.Varchar).Value = phone;
                command.Parameters.Add("@car", NpgsqlDbType.Varchar).Value = car;
                command.Parameters.Add("@carModel", NpgsqlDbType.Varchar).Value = carModel;
                command.Parameters.Add("@tuningBox", NpgsqlDbType.Integer).Value = int.Parse(tuningBox);
                command.Parameters.Add("@startDate", NpgsqlDbType.Date).Value = startDate;
                command.Parameters.Add("@endDate", NpgsqlDbType.Date).Value = endDate;
                command.Parameters.Add("@description", NpgsqlDbType.Text).Value = description;
                command.Parameters.Add("@price", NpgsqlDbType.Numeric).Value = price;
                command.Parameters.Add("@IsDone", NpgsqlDbType.Boolean).Value = false;
                
                var transaction = _db.BeginTransaction(IsolationLevel.ReadCommitted);

                _ = await command.ExecuteNonQueryAsync();
                
                await transaction.CommitAsync();
                await _db.CloseAsync();
            }
        }
    }
}
