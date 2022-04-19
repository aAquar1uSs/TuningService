using System;
using TuningService.Models;
using TuningService.Services;
using TuningService.Services.Impl;
using TuningService.Views;

namespace TuningService.Presenters;

public class OrderInfoPresenter
{
    private readonly IOrderInfoView _orderInfoView;

    private readonly TuningBoxService _tuningBoxService;

    private readonly IOrderService _orderService;

    private Order _order;

    public OrderInfoPresenter(IOrderInfoView orderInfoView,
        IOrderService orderService,
        TuningBoxService boxService)
    {
        _orderInfoView = orderInfoView;
        _orderService = orderService;
        _tuningBoxService = boxService;

        _orderInfoView.LoadFullInformationAboutOrder += GetFullInformationAboutOrder;
        _orderInfoView.ChangeStateOrderEvent += ChangeStateOrderAsync;
    }

    private async void GetFullInformationAboutOrder(object sender, int id)
    {
        try
        {
            _order = await _orderService.GetOrderByTuningBoxIdAsync(id);

            if (_order is not null)
                _order.TuningBox = await _tuningBoxService.GetFulInformationAboutTuningBoxById(id);

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
}