using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private async void GetCustomerDataAsync(object sender , int customerId)
        {
            _editCustomerView.Customer = await _customerService.GetCustomerByIdAsync(customerId);
            _editCustomerView.ShowCustomerInformation();
        }

        private async void UpdateCustomerDataAsync(object sender, EventArgs e)
        {
            await _customerService.UpdateCustomerByFullInfoAsync(_editCustomerView.Customer);
        }
    }
}
