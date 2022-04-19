using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Services;

public interface ICarService
{
    Task<Car> GetCarByTuningBoxIdAsync(int tuningBoxId);

    Task<Car> GetCarByIdAsync(int? carId);

    Task InsertNewCarAsync(Car car);

    Task<int> GetCarIdByFullInformation(Car car);
}