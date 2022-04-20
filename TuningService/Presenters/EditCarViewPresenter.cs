using System;
using TuningService.Services;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class EditCarViewPresenter
    {
        private IEditCarView _editCarView;

        private ICarService _carService;

        public EditCarViewPresenter(IEditCarView editCarView,
            ICarService carService)
        {
            _editCarView = editCarView;
            _carService = carService;

            _editCarView.GetCarDataEvent += UploadCarData;
            _editCarView.UpdateCarDataEvent += UpdateOldCarData;
        }

        private async void UploadCarData(object sender, int carId)
        {
            _editCarView.Car = await _carService.GetCarByIdAsync(carId);
            _editCarView.ShowOldData();
        }

        private async void UpdateOldCarData(object sender, EventArgs e)
        {
            await _carService.UpdateCarDataAsync(_editCarView.Car);
        }

    }
}
