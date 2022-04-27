using System.Data;
using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Services;

public interface IMasterService
{
    Task<Master> GetMasterByTuningBoxIdAsync(int tuningBoxId);

    Task<Master> GetMasterByIdAsync(int? masterId);

    Task<DataTable> GetAllMastersAsync();

    Task<int> GetMasterIdByFullInformation(Master master);

    Task<bool> InsertNewMasterAsync(Master master);

    Task<bool> DeleteMasterByFullInfo(Master master);
}