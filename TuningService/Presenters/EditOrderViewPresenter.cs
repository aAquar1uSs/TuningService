using System;
using TuningService.Services;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class EditOrderViewPresenter
    {
        private IEditOrderView _editOrderView;

        private IOrderService _orderService;

        public EditOrderViewPresenter(IEditOrderView editOrderView,
            IOrderService orderService)
        {
            _editOrderView = editOrderView;
            _orderService = orderService;

            _editOrderView.GetOrderDataEvent += GetOrderDataAsync;
            _editOrderView.UpdateOrderDataEvent += UpdateOrderDataAsync;
        }

        private async void GetOrderDataAsync(object sende, int orderId)
        {
            _editOrderView.OrderInfo = await _orderService.GetOrderByIdAsync(orderId);
            _editOrderView.ShowInformation();
        }

        private async void UpdateOrderDataAsync(object sender, EventArgs e)
        {
            await _orderService.UpdateOrderDataByFullInfo(_editOrderView.OrderInfo);
        }
    }
}
