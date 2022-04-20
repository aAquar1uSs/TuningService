using System;
using TuningService.Models;
using TuningService.Services;
using TuningService.Views;

namespace TuningService.Presenters;

public class NewOrderViewPresenter
{
    private readonly INewOrderView _newOrder;

    private readonly ICustomerService _customerService;

    private readonly IOrderService _orderService;

    private readonly IMasterService _masterService;

    private readonly ICarService _carService;

    private readonly ITuningBoxService _tuningBoxService;


    public NewOrderViewPresenter(
        INewOrderView orderView,
        ICarService carService,
        ICustomerService customerService,
        IMasterService masterService,
        IOrderService orderService,
        ITuningBoxService tuningBoxService
        )
    {
        _newOrder = orderView;
        _customerService = customerService;
        _masterService = masterService;
        _orderService = orderService;
        _carService = carService;
        _tuningBoxService = tuningBoxService;

        _newOrder.UpdateListOfMasters += UpdateMastersEvent;
        _newOrder.AddNewCustomerEvent += AddCustomerEvent;
        _newOrder.AddNewCarEvent += AddCarEvent;
        _newOrder.UproveMasterAndCreateTuningBoxEvent += UproveMasterAndCreateTuningBoxEvent;
        _newOrder.AddNewOrderEvent += AddOrderEvent;
    }

    private async void UpdateMastersEvent(object sender, EventArgs e)
    {
        var dt = await _masterService.GetAllMastersAsync();
        _newOrder.SetDataAboutMasters(dt);
    }

    private async void AddCustomerEvent(object sender, EventArgs e)
    {
        await _customerService.InsertNewCustomerAsync(_newOrder.Customer);
        _newOrder.Customer.Id = await _customerService.GetCustomerIdByFullInformation(_newOrder.Customer);
    }

    private async void AddCarEvent(object sender, EventArgs e)
    {
        await _carService.InsertNewCarAsync(_newOrder.Car);
       _newOrder.Car.Id = await _carService.GetCarIdByFullInformationAsync(_newOrder.Car);
    }

    private async void UproveMasterAndCreateTuningBoxEvent(object sender, EventArgs e)
    {
        int masterId = await _masterService.GetMasterIdByFullInformation(_newOrder.Master);

        await _tuningBoxService.InsertNewTuningBox(_newOrder.Car.Id, masterId);
        int tuningBoxId = await _tuningBoxService.GetTuningBoxIdByCarIdAsync(_newOrder.Car.Id);
        _newOrder.TuningBox = await _tuningBoxService.GetFulInformationAboutTuningBoxById(tuningBoxId);
    }

    private async void AddOrderEvent(object sender, EventArgs e)
    {
        await _orderService.InsertNewOrderAsync(_newOrder.Order);
    }
}