using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Services;

public interface ITuningBoxService
{
    Task<TuningBox> GetFulInformationAboutTuningBoxById(int tuningBoxId);

    Task InsertNewTuningBox(int carId, int masterId);

    Task<int> GetTuningBoxIdByCarIdAsync(int carId);
}