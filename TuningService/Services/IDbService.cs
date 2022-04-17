using System.Data;
using System.Threading.Tasks;

namespace TuningService.Services
{
    public interface IDbService
    {
        Task<DataTable> ShowAllDataAsync();
    }
}
