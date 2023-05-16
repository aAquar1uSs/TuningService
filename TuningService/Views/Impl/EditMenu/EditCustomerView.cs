using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuningService.Factories;
using TuningService.Models;

namespace TuningService.Views.Impl.EditMenu
{
    public partial class EditCustomerView : Form, IEditCustomerView
    {
        private int _customerId;
        private Customer _customer; 

        public EditCustomerView()
        {
            InitializeComponent();
        }

        public event UploadCustomerDelegate GetCustomerDataEvent;
        public event UpdateCustomerDelegate UpdateCustomerDataEvent;

        public void ShowCustomerInformation(Customer customer)
        {
            _customer = customer;
            textBoxSurname.Text = _customer.Surname;
            textBoxName.Text = _customer.Name;
            textBoxLastName.Text = _customer.Lastname;
            textBoxPhone.Text = _customer.Phone;
        }

        public async void GetDataAsync(int customerId)
        {
            await GetCustomerDataEvent?.Invoke(customerId);
            _customerId = customerId;
        }

        private void EditCustomerView_Load(object sender, EventArgs e)
        {
            
        }

        private async void buttonEditOwner_ClickAsync(object sender, EventArgs e)
        {
            var name = textBoxName.Text;
            var lastName = textBoxLastName.Text;
            var phone = textBoxPhone.Text;
            var surname = textBoxSurname.Text;

            try
            {
                _customer = CustomerFactory.GetCustomerInstance(name, lastName, surname, phone);
                _customer.CustomerId = _customerId;
            }
            catch (ValidationException)
            {
                MessageBox.Show("Incorrect data entered!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            await UpdateCustomerDataEvent?.Invoke(_customer);

            MessageBox.Show("Customer data has been successfully updated!",
                   "Information",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
            Close();
        }
    }
}
