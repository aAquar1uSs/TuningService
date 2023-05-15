using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Repository;

public interface ICarRepository
{
    Task<Car> GetAsync(int carId);

    Task InsertAsync(Car car);

    Task<int> GetCarIdByFullInformationAsync(Car car);

    Task UpdateAsync(Car car);
}