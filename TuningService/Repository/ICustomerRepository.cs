using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Repository;

public interface ICustomerRepository
{
    Task DeleteAsync(int customerId);

    Task<Customer?> GetAsync(int customerId);

    Task<Customer?> GetAsync(string phone);

    Task<int> InsertAsync(Customer customer);

    Task UpdateAsync(Customer customer);
}