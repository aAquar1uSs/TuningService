using System;
using System.Collections.Generic;
using System.Data;
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

    public async Task DeleteAsync(int customerId)
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
        
        return await _db.QueryFirstOrDefaultAsync<Customer>(sqlQuery, parameters, commandType: CommandType.Text);
    }

    public async Task<int> InsertAsync(Customer customer)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "INSERT INTO customer(name, surname, lastname, phone) VALUES (@name, @surname, @lastname, @phone) RETURNING cutomer_id";
        var parameters = new Dictionary<string, object>
        {
            ["name"] = customer.Name,
            ["surname"] = customer.Surname,
            ["lastname"] = customer.Lastname,
            ["phone"] = customer.Phone
        };

        return await _db.QueryFirstOrDefaultAsync<int>(sqlQuery, parameters, commandType: CommandType.Text);
    }

    public async Task UpdateAsync(Customer customer)
    {
        if (_db.State == ConnectionState.Closed)
            _db.Open();

        var sqlQuery = "UPDATE customer SET name = @name, lastname = @lastname, surname = @surname, phone = @phone WHERE customer_id = @customerId;";
        var parameters = new Dictionary<string, object>
        {
            ["customerId"] = customer.CustomerId,
            ["name"] = customer.Name,
            ["surname"] = customer.Surname,
            ["lastname"] = customer.Lastname,
            ["phone"] = customer.Phone
        };
        
        await _db.QueryAsync(sqlQuery, parameters, commandType: CommandType.Text);
    }
}
