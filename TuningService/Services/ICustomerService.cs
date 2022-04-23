using System.Data;
using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Services;

public interface ICustomerService
{
    Task DeleteCustomerByIdAsync(int customerId);
    
    Task<DataTable> SearchCustomerByValueAsync(string value);

    Task<Customer> GetCustomerByIdAsync(int customerId);

    Task InsertNewCustomerAsync(Customer customer);

    Task<int> GetCustomerIdByFullInformationAsync(Customer customer);

    Task<int> GetCustomerIdByCarIdAsync(int carId);

    Task UpdateCustomerByFullInfoAsync(Customer customer);
}