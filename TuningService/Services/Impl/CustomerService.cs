using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;

namespace TuningService.Services.Impl;

public class CustomerService : ICustomerService
{
    private readonly NpgsqlConnection _sqlConnection;
    
    private const string SqlRequestSearchCustomer = "SELECT customer.customer_id,"
                                                     + "concat(customer.surname,' ', customer.name, ' ', customer.lastname), customer.phone,"
                                                     + "car.car_id, concat(car.name, ' ', car.model), tuning_box.box_id,"
                                                     + "concat(master.name, ' ', master.surname), master.phone "
                                                     + "FROM customer JOIN car ON customer.customer_id = car.customer_id "
                                                     + "JOIN tuning_box ON car.car_id = tuning_box.car_id "
                                                     + "JOIN master ON tuning_box.master_id = master.master_id "
                                                     + "WHERE customer.customer_id = @customerId or customer.name = @name "
                                                     + "or customer.surname = @surname or customer.lastname = @lastname "
                                                     + "or customer.phone = @phone"; 

    public CustomerService(string sqlConnectionString)
    {
        _sqlConnection = new NpgsqlConnection(sqlConnectionString);
    }

    public async Task DeleteCustomerByIdAsync(int customerId)
    {
        await _sqlConnection.OpenAsync();
        using (var command = new NpgsqlCommand())
        {
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM customer WHERE customer_id = @id";
            command.Parameters.Add("@id", NpgsqlDbType.Integer).Value = customerId;

            await using (_ = await command.ExecuteReaderAsync());
        }

        await _sqlConnection.CloseAsync();
    }

    public async Task<DataTable> SearchCustomerByValue(string value)
    {
        var dataTable = new DataTable();

        var customerId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
        var name = value;
        var surname = value;
        var lastname = value;
        var phone = value;

        await _sqlConnection.OpenAsync();

        using (var command = new NpgsqlCommand())
        {
            command.Connection = _sqlConnection;
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

        await _sqlConnection.CloseAsync();
        return dataTable;
    }
}