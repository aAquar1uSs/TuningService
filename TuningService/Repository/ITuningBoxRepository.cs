using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Repository;

public interface ITuningBoxRepository
{
    Task<TuningBox?> GetFulInformationAboutTuningBoxById(int tuningBoxId);

    Task InsertAsync(TuningBox box);

    Task<int> GetTuningBoxIdByCarIdAsync(int carId);

    Task<bool> VerifyBoxNumberAsync(int boxNumber);

    Task UpdateMasterIdAsync(int oldId, int newId);
}