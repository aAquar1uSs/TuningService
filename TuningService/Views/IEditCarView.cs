using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Views
{
    public delegate Task UpdateCarDelegate(Car car);
    public delegate Task UploadCarDelegate(int carId);

    public interface IEditCarView
    {
        event UploadCarDelegate GetCarDataEvent;

        event UpdateCarDelegate UpdateCarDataEvent;

        public void GetCarDataAsync(int carId);

        public void ShowOldData(Car car);
    }
}
