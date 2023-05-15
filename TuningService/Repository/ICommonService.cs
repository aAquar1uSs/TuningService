using System.Data;
using System.Threading.Tasks;

namespace TuningService.Repository
{
    public interface ICommonService
    {
        Task<DataTable> ShowAllDataAsync();

        Task<DataTable> SearchCustomerByValueAsync(string value);

        Task Insert(DataTable dataTable);
    }
}
