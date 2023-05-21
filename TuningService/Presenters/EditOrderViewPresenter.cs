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
    public class EditOrderViewPresenter
    {
        private readonly IEditOrderView _editOrderView;
        private readonly IOrderRepository _orderRepository;

        public EditOrderViewPresenter(IEditOrderView editOrderView)
        {
            _editOrderView = editOrderView;
            _orderRepository = new OrderRepository(new NpgsqlConnection(AppConnection.ConnectionString));

            _editOrderView.GetOrderDataEvent += GetOrderDataAsync;
            _editOrderView.UpdateOrderDataEvent += UpdateOrderDataAsync;
        }

        private async Task GetOrderDataAsync(int orderId)
        {
            var order = await _orderRepository.GetAsync(orderId);
            _editOrderView.ShowInformation(order);
        }

        private async Task UpdateOrderDataAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);
        }
    }
}
