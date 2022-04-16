using System.Data;
using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Services
{
    public interface IDbService
    {
        Task<DataTable> ShowAllDataAsync();

        Task DeleteCustomerByIdAsync(int customerId);

        Task<Order> GetOrderByTuningBoxIdAsync(int tuningBoxId);

        Task<Master> GetMasterByTuningBoxIdAsync(int tuningBoxId);

        Task<TuningBox> GetFulInformationAboutTuningBoxById(int tuningBoxId);
    }
}
