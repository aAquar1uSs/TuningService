using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Services;

public interface ITuningBoxService
{
    Task<TuningBox> GetFulInformationAboutTuningBoxById(int tuningBoxId);

    Task<bool> InsertNewTuningBoxAsync(TuningBox box);

    Task<int> GetTuningBoxIdByCarIdAsync(int carId);

    Task<bool> VerifyBoxNumberAsync(int boxNumber);

    Task<bool> UpdateMasterIdAsync(int oldId, int newId);
}