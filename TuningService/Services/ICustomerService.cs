using System.Data;
using System.Threading.Tasks;

namespace TuningService.Services;

public interface ICustomerService
{
    Task DeleteCustomerByIdAsync(int customerId);
    
    Task<DataTable> SearchCustomerByValue(string value);
}