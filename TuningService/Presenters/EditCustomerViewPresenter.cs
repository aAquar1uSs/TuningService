using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using TuningService.Models;
using TuningService.Repository;
using TuningService.Repository.Impl;
using TuningService.Utilites.Settings;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class EditCustomerViewPresenter
    {
        private readonly IEditCustomerView _editCustomerView;
        private readonly ICustomerRepository _customerRepository;

        public EditCustomerViewPresenter(IEditCustomerView editCustomerView)
        {
            _editCustomerView = editCustomerView;
            _customerRepository = new CustomerRepository(new NpgsqlConnection(AppConnection.ConnectionString));

            _editCustomerView.GetCustomerDataEvent += GetCustomerDataAsync;
            _editCustomerView.UpdateCustomerDataEvent += UpdateCustomerDataAsync;
        }

        private async Task GetCustomerDataAsync(int customerId)
        {
            var customer = await _customerRepository.GetAsync(customerId);
            _editCustomerView.ShowCustomerInformation(customer);
        }

        private async Task UpdateCustomerDataAsync(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }
    }
}
