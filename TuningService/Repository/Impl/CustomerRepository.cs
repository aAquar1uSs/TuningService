using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using TuningService.Models;

namespace TuningService.Repository.Impl;

public class CustomerRepository : ICustomerRepository
{
    private readonly NpgsqlConnection _db;
    
    public CustomerRepository(NpgsqlConnection db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task DeleteByIdAsync(int customerId)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "DELETE FROM customer WHERE customer_id = @id";
        var parameters = new Dictionary<string, object>
        {
            ["id"] = customerId
        };
        
        await _db.QueryAsync(sqlQuery, parameters, commandType: CommandType.Text);
    }
    

    public async Task<Customer?> GetAsync(int customerId)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "SELECT * FROM customer WHERE customer_id = @id";
        var parameters = new Dictionary<string, object>
        {
            ["id"] = customerId
        };
        
        return (await _db.QueryAsync<Customer>(sqlQuery, parameters, commandType: CommandType.Text)).FirstOrDefault();
    }

    public async Task InsertAsync(Customer customer)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "INSERT INTO customer(name, surname, lastname, phone) VALUES (@name, @surname, @lastname, @phone)";
        var parameters = new Dictionary<string, object>
        {
            ["name"] = customer.Name,
            ["surname"] = customer.Surname,
            ["lastname"] = customer.Lastname,
            ["phone"] = customer.Phone
        };

        await _db.QueryAsync(sqlQuery, parameters, commandType: CommandType.Text);
    }

    public async Task<int> GetCustomerIdByFullInformationAsync(Customer customer)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "SELECT customer_id FROM customer WHERE name = @name AND surname = @surname AND lastname = @lastname AND phone = @phone";
        var parameters = new Dictionary<string, object>
        {
            ["name"] = customer.Name,
            ["surname"] = customer.Surname,
            ["lastname"] = customer.Lastname,
            ["phone"] = customer.Phone
        };

        return (await _db.QueryAsync<int>(sqlQuery, parameters, commandType: CommandType.Text)).FirstOrDefault();
    }
    
    public async Task UpdateAsync(Customer customer)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "UPDATE customer SET name = @name, lastname = @lastname, surname = @surname, phone = @phone WHERE customer_id = @customerId;";
        var parameters = new Dictionary<string, object>
        {
            ["customerId"] = customer.Id,
            ["name"] = customer.Name,
            ["surname"] = customer.Surname,
            ["lastname"] = customer.Lastname,
            ["phone"] = customer.Phone
        };
        
        await _db.QueryAsync(sqlQuery, parameters, commandType: CommandType.Text);
    }
}
