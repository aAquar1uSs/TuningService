using TuningService.Models;
using TuningService.Services;
using TuningService.Services.Impl;
using TuningService.Views;

namespace TuningService.Presenters;

public class OrderInfoPresenter
{
    private readonly IOrderInfoView _orderInfoView;

    private readonly TuningBoxService _tuningBoxService;

    private TuningBox _tuningBox;

    public OrderInfoPresenter(IOrderInfoView orderInfoView,
        IOrderService orderService, IMasterService masterService)
    {
        _orderInfoView = orderInfoView;
        _tuningBoxService = new TuningBoxService(orderService, masterService);

        _orderInfoView.LoadFullInformationAboutOrder += GetFullInformationAboutOrder;
    }

    private async void GetFullInformationAboutOrder(object sender, int id)
    {
        _tuningBox = await _tuningBoxService.GetFulInformationAboutTuningBoxById(id);
        _orderInfoView.ShowInformationAboutOrder(_tuningBox);
    }


}