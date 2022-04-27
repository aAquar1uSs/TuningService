using System.Threading.Tasks;
using System.Windows.Forms;
using TuningService.Models;
using TuningService.Services;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class EditCarViewPresenter
    {
        private readonly IEditCarView _editCarView;

        private readonly ICarService _carService;

        public EditCarViewPresenter(IEditCarView editCarView,
            ICarService carService)
        {
            _editCarView = editCarView;
            _carService = carService;

            _editCarView.GetCarDataEvent += UploadCarData;
            _editCarView.UpdateCarDataEvent += UpdateOldCarData;
        }

        private async Task UploadCarData(int carId)
        {
            var car = await _carService.GetCarByIdAsync(carId);
            _editCarView.ShowOldData(car);
        }

        private async Task UpdateOldCarData(Car car)
        {
            if (!await _carService.UpdateCarDataAsync(car))
            {
                MessageBox.Show("An unexpected error has occurred!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

    }
}
