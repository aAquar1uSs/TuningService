using System;
using System.Windows.Forms;
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

        private async void GetOrderDataAsync(object sender, int orderId)
        {
            _editOrderView.OrderInfo = await _orderService.GetOrderByIdAsync(orderId);
            _editOrderView.ShowInformation();
        }

        private async void UpdateOrderDataAsync(object sender, EventArgs e)
        {
            if (!await _orderService.UpdateOrderDataByFullInfo(_editOrderView.OrderInfo))
            {
                MessageBox.Show("An unexpected error has occurred!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
