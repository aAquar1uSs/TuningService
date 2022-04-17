using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Services;

public interface ITuningBoxService
{
    Task<TuningBox> GetFulInformationAboutTuningBoxById(int tuningBoxId);
}