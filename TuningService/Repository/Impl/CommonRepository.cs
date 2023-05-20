using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using NpgsqlTypes;
using TuningService.Models.ViewModels;

namespace TuningService.Repository.Impl
{
    public sealed class CommonRepository : ICommonRepository
    {
        private readonly NpgsqlConnection _db;
        
        private const string SqlRequestSearchInfo = "SELECT customer.customer_id AS CustomerId,"
                                                        + "concat(customer.surname,' ', customer.name, ' ', customer.lastname) AS CustomerName, customer.phone AS CustomerPhone,"
                                                        + "car.car_id  AS CarId, concat(car.brand, ' ', car.model) AS CarModel, tuning_box.box_id AS BoxId,"
                                                        + "master.master_id AS MasterId, concat(master.name, ' ', master.surname) AS MasterName, master.phone AS MasterPhone "
                                                        + "FROM customer JOIN car ON customer.customer_id = car.customer_id "
                                                        + "JOIN tuning_box ON car.car_id = tuning_box.car_id "
                                                        + "JOIN master ON tuning_box.master_id = master.master_id "
                                                        + "WHERE customer.customer_id = @customerId or customer.name = @name "
                                                        + "or customer.surname = @surname or customer.lastname = @lastname "
                                                        + "or customer.phone = @phone";

        public CommonRepository(NpgsqlConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<IReadOnlyCollection<ComparedDataView>> ShowAllDataAsync()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();

            var sqlQuery = "SELECT customer.customer_id AS CustomerId, " +
                           "concat(customer.surname, ' ', customer.name, ' ', customer.lastname) AS CustomerName, customer.phone AS CustomerPhone, " +
                           "car.car_id AS CarId, concat(car.brand, ' ', car.model) AS CarModel, " +
                           "tuning_box.box_id AS BoxId, " +
                           "master.master_id AS MasterId, concat(master.name, ' ', master.surname) AS MasterName, master.phone AS MasterPhone " +
                           "FROM customer " +
                           "JOIN car ON customer.customer_id = car.customer_id " +
                           "JOIN tuning_box ON car.car_id = tuning_box.car_id " +
                           "JOIN master ON tuning_box.master_id = master.master_id";

            var result = await _db.QueryAsync<ComparedDataView>(sqlQuery, commandType: CommandType.Text);
            return result.ToArray();
        }
        
        public async Task<IReadOnlyCollection<ComparedDataView>> SearchCustomerByValueAsync(string value)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            
            var customerId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            var name = value;
            var surname = value;
            var lastname = value;
            var phone = value;
            
            var parameters = new Dictionary<string, object>
            {
                ["customerId"] = customerId,
                ["name"] = name,
                ["surname"] = surname,
                ["lastname"] = lastname,
                ["phone"] = phone
            };
            
            var result = await _db.QueryAsync<ComparedDataView>(SqlRequestSearchInfo, parameters, commandType: CommandType.Text);

            return result.ToArray();
        }

        public async Task Insert(IReadOnlyCollection<DataForImport> data)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            
            foreach (var model in data)
            {
                /*command.CommandText = "with first_customer_insert as ( " +
                    "insert into customer(name,lastname,surname,phone) " + 
                    "values(@name,@lastname,@surname,@phone)" + 
                    "RETURNING customer_id ), " + 
                "second_car_insert as ( " +
                "insert into car(brand ,model,customer_id) " +
                "values " +
                "(@car,@carModel,(select customer_id from first_customer_insert)) " +
                "RETURNING car_id), " +
                "third_tuning_box_insert as ( " +
                "insert into tuning_box(box_number,master_id,car_id) " +
                "values (@tuningBox,(select master_id from master limit 1),(select car_id from second_car_insert)) " +
                "RETURNING tuning_box_id), " +
                "insert into tuning_order(start_date,end_date,description,price,is_done,tuning_box_id) " + 
                "values " +
                "(@startDate,@endDate,@description,@price,@isDone,(select tuning_box_id from third_tuning_box_insert));";*/
                
                string query = @"
                    with first_customer_insert as (
        insert into customer(name, lastname, surname, phone)
        values (@name, @lastname, @surname, @phone)
        returning customer_id
    ),
    second_car_insert as (
        insert into car(name, model, customer_id)
        values (@car, @carModel, (select customer_id from first_customer_insert))
        returning car_id
    ),
    third_tuning_box_insert as (
        insert into tuning_box(box_number, master_id, car_id)
        values (@tuningBox, (select master_id from master limit 1), (select car_id from second_car_insert))
        returning tuning_box_id
    )
    insert into tuning_order(start_date, end_date, description, price, is_done, tuning_box_id)
    values (@startDate, @endDate, @description, @price, @isDone, (select tuning_box_id from third_tuning_box_insert));
";
                
                var parameters = new
                {
                    name = model.CustomerName,
                    lastname = model.CustomerLastname,
                    surname = model.CustomerSurname,
                    phone = model.CustomerPhone,
                    brand = model.CarBrand,
                    model = model.CarModel,
                    tuningBox = model.BoxNumber,
                    startDate = model.StartDate,
                    endDate = model.EndDate,
                    description = model.Description,
                    price = model.Price,
                    isDone = false
                };
                
                await _db.ExecuteAsync(query, parameters);
            }
        }
    }
}
