using System.Threading.Tasks;
using System.Windows.Forms;
using TuningService.Models;
using TuningService.Repository;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class EditOrderViewPresenter
    {
        private readonly IEditOrderView _editOrderView;
        private readonly IOrderRepository _orderRepository;

        public EditOrderViewPresenter(IEditOrderView editOrderView,
            IOrderRepository orderRepository)
        {
            _editOrderView = editOrderView;
            _orderRepository = orderRepository;

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
