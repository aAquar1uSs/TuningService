using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Services;

public interface IMasterService
{
    Task<Master> GetMasterByTuningBoxIdAsync(int tuningBoxId);

    Task<Master> GetMasterByIdAsync(int? masterId);
}