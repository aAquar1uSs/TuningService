using System.Data;
using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Repository;

public interface IMasterRepository
{
    Task<DataTable> GetAllAsync();

    Task<int> GetMasterIdAsync(Master master);

    Task InsertAsync(Master master);

    Task DeleteAsync(Master master);
}