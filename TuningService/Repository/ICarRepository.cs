using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Repository;

public interface ICarRepository
{
    Task<Car> GetAsync(int carId);

    Task<int> InsertAsync(Car car);

    Task UpdateAsync(Car car);
}