using System.Data;
using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Services;

public interface ICustomerService
{
    Task DeleteCustomerByIdAsync(int customerId);
    
    Task<DataTable> SearchCustomerByValue(string value);

    Task<Customer> GetCustomerByIdAsync(int customerId);

    Task InsertNewCustomerAsync(Customer customer);

    Task<int> GetCustomerIdByFullInformation(Customer customer);

    Task<Customer> GetCustomerByCarId(int carId);
}