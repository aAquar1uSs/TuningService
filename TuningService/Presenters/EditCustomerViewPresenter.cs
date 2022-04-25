using System.Threading.Tasks;
using System.Windows.Forms;
using TuningService.Models;
using TuningService.Services;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class EditCustomerViewPresenter
    {
        private readonly IEditCustomerView _editCustomerView;

        private readonly ICustomerService _customerService;

        public EditCustomerViewPresenter(IEditCustomerView editCustomerView,
            ICustomerService customerService)
        {
            _editCustomerView = editCustomerView;
            _customerService = customerService;

            _editCustomerView.GetCustomerDataEvent += GetCustomerDataAsync;
            _editCustomerView.UpdateCustomerDataEvent += UpdateCustomerDataAsync;
        }

        private async Task GetCustomerDataAsync(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            _editCustomerView.ShowCustomerInformation(customer);
        }

        private async Task UpdateCustomerDataAsync(Customer customer)
        {
            if (!await _customerService.UpdateCustomerByFullInfoAsync(customer))
            {
                MessageBox.Show("An unexpected error has occurred!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
