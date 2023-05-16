using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Repository;

public interface ITuningBoxRepository
{
    Task<TuningBox?> GetAsync(int tuningBoxId);

    Task InsertAsync(TuningBox box);

    Task<int> GetTuningBoxIdByCarIdAsync(int carId);

    Task UpdateMasterIdAsync(int oldId, int newId);
}