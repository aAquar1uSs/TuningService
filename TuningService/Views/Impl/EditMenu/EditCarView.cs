using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;
using TuningService.Factories;
using TuningService.Models;

namespace TuningService.Views.Impl.EditMenu
{
    public partial class EditCarView : Form, IEditCarView
    {
        public EditCarView()
        {
            InitializeComponent();
        }

        private Car _car;

        public Car Car { get => _car; set => _car = value; }

        public event EventHandler<int> GetCarDataEvent;
        public event EventHandler UpdateCarDataEvent;

        public void GetCarData(int carId)
        {
            GetCarDataEvent?.Invoke(this, carId);
        }

        private void EditCarView_Load(object sender, EventArgs e)
        {

        }

        public void ShowOldData()
        {
            textBoxName.Text = _car.Name;
            textBoxModel.Text = _car.Model;
        }

        private void buttonEditCar_Click(object sender, EventArgs e)
        {
            var id = _car.Id;//Save car id;
            try
            {
                _car = CarFactory.GetCarInstance(textBoxName.Text, textBoxModel.Text);
                _car.Id = id;
            }
            catch (ValidationException)
            {
                MessageBox.Show("Incorrect data entered!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            UpdateCarDataEvent?.Invoke(this, EventArgs.Empty);

            MessageBox.Show("Car data has been successfully updated!",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            Close();
        }
    }
}
