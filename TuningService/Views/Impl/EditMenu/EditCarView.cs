﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;
using TuningService.Factories;
using TuningService.Models;

namespace TuningService.Views.Impl.EditMenu
{
    public partial class EditCarView : Form, IEditCarView
    {
        private int _id;
        private Car _car;

        public EditCarView()
        {
            InitializeComponent();
        }


        public event UploadCarDelegate GetCarDataEvent;
        public event UpdateCarDelegate UpdateCarDataEvent;

        public async void GetCarDataAsync(int carId)
        {
            _id = carId;
            await GetCarDataEvent?.Invoke(carId);
        }

        private void EditCarView_Load(object sender, EventArgs e)
        {

        }

        public void ShowOldData(Car car)
        {
            textBoxName.Text = car.Brand;
            textBoxModel.Text = car.Model;
        }

        private async void buttonEditCar_ClickAsync(object sender, EventArgs e)
        {
            var carResult = CarFactory.GetCarInstance(textBoxName.Text, textBoxModel.Text);
            if (carResult.IsFailure)
            {
                MessageBox.Show("Incorrect data entered!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _car = carResult.Value;
            _car.CarId = _id;

            await UpdateCarDataEvent?.Invoke(_car);

            MessageBox.Show("Car data has been successfully updated!",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            Close();
        }
    }
}
