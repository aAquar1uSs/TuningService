using System.Collections.Generic;
using System.Threading.Tasks;
using TuningService.Models;
using TuningService.Models.ViewModels;

namespace TuningService.Repository;

public interface IMasterRepository
{
    Task<IReadOnlyCollection<MasterViewModel>> GetAllAsync();

    Task<int> GetMasterIdAsync(Master master);

    Task<int> InsertAsync(Master master);

    Task DeleteAsync(Master master);
}