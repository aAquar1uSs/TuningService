using System;
using System.Threading.Tasks;
using TuningService.Models;
using TuningService.Services;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class EditCustomerViewPresenter
    {
        private IEditCustomerView _editCustomerView;

        private ICustomerService _customerService;

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
            await _customerService.UpdateCustomerByFullInfoAsync(customer);
        }
    }
}
