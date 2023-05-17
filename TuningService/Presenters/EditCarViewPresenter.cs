using System.Threading.Tasks;
using System.Windows.Forms;
using TuningService.Models;
using TuningService.Repository;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class EditCarViewPresenter
    {
        private readonly IEditCarView _editCarView;
        private readonly ICarRepository _carRepository;

        public EditCarViewPresenter(IEditCarView editCarView, ICarRepository carRepository)
        {
            _editCarView = editCarView;
            _carRepository = carRepository;

            _editCarView.GetCarDataEvent += UploadCarData;
            _editCarView.UpdateCarDataEvent += UpdateOldCarData;
        }

        private async Task UploadCarData(int carId)
        {
            var car = await _carRepository.GetAsync(carId);
            _editCarView.ShowOldData(car);
        }

        private async Task UpdateOldCarData(Car car)
        {
            await _carRepository.UpdateAsync(car);
        }

    }
}
