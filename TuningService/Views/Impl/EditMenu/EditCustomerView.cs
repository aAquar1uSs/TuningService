using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;
using TuningService.Factories;
using TuningService.Models;

namespace TuningService.Views.Impl.EditMenu
{
    public partial class EditCustomerView : Form, IEditCustomerView
    {
        private Customer _customer; 

        public EditCustomerView()
        {
            InitializeComponent();
        }

        public Customer Customer { get => _customer; set => _customer = value; }

        public event EventHandler<int> GetCustomerDataEvent;
        public event EventHandler UpdateCustomerDataEvent;

        public void ShowCustomerInformation()
        {
            textBoxSurname.Text = _customer.Surname;
            textBoxName.Text = _customer.Name;
            textBoxLastName.Text = _customer.Lastname;
            textBoxPhone.Text = _customer.Phone;
        }

        public void GetData(int customerId)
        {
            GetCustomerDataEvent?.Invoke(this, customerId);
        }

        private void EditCustomerView_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonEditOwner_Click(object sender, EventArgs e)
        {
            var name = textBoxName.Text;
            var lastName = textBoxLastName.Text;
            var phone = textBoxPhone.Text;
            var surname = textBoxSurname.Text;

            var id = _customer.Id;
            try
            {
                _customer = CustomerFactory.GetCustomerInstance(name, lastName, surname, phone);
                _customer.Id = id;
            }
            catch (ValidationException)
            {
                MessageBox.Show("Incorrect data entered!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            UpdateCustomerDataEvent?.Invoke(this, EventArgs.Empty);

            MessageBox.Show("Customer data has been successfully updated!",
                   "Information",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
            Close();
        }
    }
}
