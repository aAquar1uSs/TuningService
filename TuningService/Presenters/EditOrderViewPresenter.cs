using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuningService.Models;
using TuningService.Services;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class EditOrderViewPresenter
    {
        private readonly IEditOrderView _editOrderView;

        private readonly IOrderService _orderService;

        public EditOrderViewPresenter(IEditOrderView editOrderView,
            IOrderService orderService)
        {
            _editOrderView = editOrderView;
            _orderService = orderService;

            _editOrderView.GetOrderDataEvent += GetOrderDataAsync;
            _editOrderView.UpdateOrderDataEvent += UpdateOrderDataAsync;
        }

        private async Task GetOrderDataAsync(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            _editOrderView.ShowInformation(order);
        }

        private async Task UpdateOrderDataAsync(Order order)
        {
            if (!await _orderService.UpdateOrderDataByFullInfo(order))
            {
                MessageBox.Show("An unexpected error has occurred!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
