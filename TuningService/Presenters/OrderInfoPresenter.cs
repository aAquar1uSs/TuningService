using System;
using TuningService.Models;
using TuningService.Services;
using TuningService.Services.Impl;
using TuningService.Views;
using TuningService.Views.Impl.EditMenu;

namespace TuningService.Presenters;

public class OrderInfoPresenter
{
    private readonly IOrderInfoView _orderInfoView;

    private readonly TuningBoxService _tuningBoxService;

    private readonly IOrderService _orderService;

    private Order _order;

    private readonly string _connectionString;

    public OrderInfoPresenter(IOrderInfoView orderInfoView,
        IOrderService orderService,
        TuningBoxService boxService,
        string connectionString)
    {
        _orderInfoView = orderInfoView;
        _orderService = orderService;
        _tuningBoxService = boxService;
        _connectionString = connectionString;

        _orderInfoView.LoadFullInformationAboutOrder += GetFullInformationAboutOrder;
        _orderInfoView.ChangeStateOrderEvent += ChangeStateOrderAsync;
        _orderInfoView.ShowEditCarEvent += ShowEditCarView;
        _orderInfoView.ShowEditCustomerEvent += ShowEditCustomerView;
        _orderInfoView.ShowEditOrderEvent += ShowEditOrderView;
    }

    private async void GetFullInformationAboutOrder(object sender, EventArgs e)
    {
        try
        {
            _order = await _orderService.GetOrderByTuningBoxIdAsync(_orderInfoView.TuningBoxId);

            if (_order is not null)
                _order.TuningBox = await _tuningBoxService.GetFulInformationAboutTuningBoxById(_orderInfoView.TuningBoxId);

            _orderInfoView.ShowInformationAboutOrder(_order);
        }
        catch (InvalidOperationException)
        {
            return;
        }
    }

    private async void ChangeStateOrderAsync(object sender, EventArgs e)
    {
        if (_order is null)
            return;

        await _orderService.ChangeStateOrderByInstance(_order);
        _orderInfoView.ShowInformationAboutOrder(_order);
    }

    private void ShowEditCarView(object sender, int carId)
    {
        var editCarView = new EditCarView();

        var carService = new CarService(_connectionString);

        _ = new EditCarViewPresenter(editCarView, carService);
        editCarView.GetCarDataAsync(carId);
        editCarView.ShowDialog();
    }

    private void ShowEditCustomerView(object sender, int customerId)
    {
        var editCustomerView = new EditCustomerView();
        var customerService = new CustomerService(_connectionString);

        _ = new EditCustomerViewPresenter(editCustomerView, customerService);
        editCustomerView.GetDataAsync(customerId);
        editCustomerView.ShowDialog();
    }

    private void ShowEditOrderView(object sender, int orderId)
    {
        var editOrderView = new EditOrderView();
        var orderService = new OrderService(_connectionString);

        _ = new EditOrderViewPresenter(editOrderView, orderService);
        editOrderView.GetOldOrderData(orderId);
        editOrderView.ShowDialog();
    }
}