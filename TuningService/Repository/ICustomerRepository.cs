using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Repository;

public interface ICustomerRepository
{
    Task DeleteByIdAsync(int customerId);

    Task<Customer?> GetAsync(int customerId);

    Task InsertAsync(Customer customer);

    Task<int> GetCustomerIdByFullInformationAsync(Customer customer);

    Task UpdateAsync(Customer customer);
}