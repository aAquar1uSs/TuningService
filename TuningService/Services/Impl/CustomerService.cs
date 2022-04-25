using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using TuningService.Factories;
using TuningService.Models;

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

    public async Task<bool> DeleteCustomerByIdAsync(int customerId)
    {
        try
        {
            await _sqlConnection.OpenAsync();
            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM customer WHERE customer_id = @id";
                command.Parameters.Add("@id", NpgsqlDbType.Integer).Value = customerId;

                await using (_ = await command.ExecuteReaderAsync()) { };
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

    public async Task<DataTable> SearchCustomerByValueAsync(string value)
    {
        var dataTable = new DataTable();

        var customerId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
        var name = value;
        var surname = value;
        var lastname = value;
        var phone = value;

        try
        {
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
        }
        catch (NpgsqlException)
        {
            await _sqlConnection.CloseAsync();
            return dataTable;
        }

        return dataTable;
    }

    public async Task<Customer> GetCustomerByIdAsync(int customerId)
    {
        Customer customer = null;
        try
        {
            await _sqlConnection.OpenAsync();
            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM customer WHERE customer_id = @id";
                command.Parameters.Add("@id", NpgsqlDbType.Integer).Value = customerId;

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        customer = CustomerFactory.GetCustomerInstance(reader);
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

        return customer;
    }

    public async Task InsertNewCustomerAsync(Customer customer)
    {
        try
        {
            await _sqlConnection.OpenAsync();
            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO customer(name, surname, lastname, phone) "
                    + "VALUES (@name, @surname, @lastname, @phone)";
                command.Parameters.Add("@name", NpgsqlDbType.Varchar).Value = customer.Name;
                command.Parameters.Add("@surname", NpgsqlDbType.Varchar).Value = customer.Surname;
                command.Parameters.Add("@lastname", NpgsqlDbType.Varchar).Value = customer.Lastname;
                command.Parameters.Add("@phone", NpgsqlDbType.Varchar).Value = customer.Phone;

                await using (var reader = await command.ExecuteReaderAsync()) { };
            }

            await _sqlConnection.CloseAsync();
        }
        catch (NpgsqlException)
        {
            await _sqlConnection.CloseAsync();
        }
    }

    public async Task<int> GetCustomerIdByFullInformationAsync(Customer customer)
    {
        var customerId = 0;

        try
        {
            await _sqlConnection.OpenAsync();
            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT customer_id FROM customer "
                    + "WHERE name = @name AND surname = @surname AND lastname = @lastname AND phone = @phone";
                command.Parameters.Add("@name", NpgsqlDbType.Varchar).Value = customer.Name;
                command.Parameters.Add("@surname", NpgsqlDbType.Varchar).Value = customer.Surname;
                command.Parameters.Add("@lastname", NpgsqlDbType.Varchar).Value = customer.Lastname;
                command.Parameters.Add("@phone", NpgsqlDbType.Varchar).Value = customer.Phone;

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        customerId = reader.GetInt32(0);
                    }
                }
            }

            await _sqlConnection.CloseAsync();
        }
        catch (NpgsqlException)
        {
            await _sqlConnection.CloseAsync();
            return 0;
        }

        return customerId;
    }

    public async Task<int> GetCustomerIdByCarIdAsync(int carId)
    {
        var customerId = 0;

        try
        {
            await _sqlConnection.OpenAsync();
            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT car.customer_id FROM car WHERE  car_id = @carId";
                command.Parameters.Add("@carId", NpgsqlDbType.Integer).Value = carId;


                await using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        customerId = reader.GetInt32(0);
                    }
                }
            }

            await _sqlConnection.CloseAsync();
        }
        catch (NpgsqlException)
        {
            await _sqlConnection.CloseAsync();
            return 0;
        }

        return customerId;
    }

    public async Task<bool> UpdateCustomerByFullInfoAsync(Customer customer)
    {
        try
        {
            await _sqlConnection.OpenAsync();

            using (var command = new NpgsqlCommand())
            {
                command.Connection = _sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE customer"
                    + " SET name = @name, lastname = @lastname, surname = @surname, phone = @phone"
                    + " WHERE customer_id = @customerId;";
                command.Parameters.Add("@name", NpgsqlDbType.Varchar).Value = customer.Name;
                command.Parameters.Add("@lastname", NpgsqlDbType.Varchar).Value = customer.Lastname;
                command.Parameters.Add("@surname", NpgsqlDbType.Varchar).Value = customer.Surname;
                command.Parameters.Add("@phone", NpgsqlDbType.Varchar).Value = customer.Phone;
                command.Parameters.Add("@customerId", NpgsqlDbType.Integer).Value = customer.Id;

                await using (var reader = await command.ExecuteReaderAsync()) { };
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
