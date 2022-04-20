using System;
using TuningService.Services;
using TuningService.Services.Impl;
using TuningService.Views;
using TuningService.Views.Impl;

namespace TuningService.Presenters;

public class MainPresenter
{
    private readonly IMainView _mainView;

    private readonly IDbService _dbService;

    private readonly ICustomerService _customerService;

    private readonly string _connectionString;

    public MainPresenter(IMainView mainView, string sqlConnection,
        IDbService dbService, ICustomerService customerService)
    {
        _mainView = mainView;
        _dbService = dbService;
        _customerService = customerService;
        _connectionString = sqlConnection;

        _mainView.ShowOrderInfoViewEvent += ShowOrderInfoViewEvent;
        _mainView.UpdateAllDataEvent += ShowAllDataEvent;
        _mainView.RemoveDataFromTableEvent += RemoveData;
        _mainView.SearchEvent += SearchCustomer;
        _mainView.ShowNewOrderViewEvent += ShowNewOrderViewEvent;
        _mainView.ShowNewMasterView += ShowNewMasterViewEvent;
        _mainView.ShowDeleteMasterView += ShowDeleteMasterViewEvent;
    }

    private void ShowOrderInfoViewEvent(object sender, int index)
    {
        var orderService = new OrderService(_connectionString);
        var masterService = new MasterService(_connectionString);
        var carService = new CarService(_connectionString);

        var tuningBoxService = new TuningBoxService(_connectionString, carService,
            masterService, _customerService);

        var orderInfoView = OrderInfoView.GetInstance();
        _ = new OrderInfoPresenter(orderInfoView, orderService,
            tuningBoxService,_connectionString);
        orderInfoView.LoadOrderAsync(index);
        orderInfoView.Show();
    }

    private void ShowNewOrderViewEvent(object sender, EventArgs e)
    {
        var newOrderView = NewOrderView.GetInstance();

        var orderService = new OrderService(_connectionString);
        var masterService = new MasterService(_connectionString);
        var carService = new CarService(_connectionString);
        var tuningBoxService = new TuningBoxService(_connectionString, carService, 
            masterService, _customerService);

        _ = new NewOrderViewPresenter(newOrderView,carService,
            _customerService, masterService, orderService, tuningBoxService);

        newOrderView.ShowDialog();
    }

    private void ShowNewMasterViewEvent(object sender, EventArgs e)
    {
        var newMasterView = NewMasterView.GetInstance();
        var masterService = new MasterService(_connectionString);
        _ = new NewMasterViewPresenter(newMasterView, masterService);

        newMasterView.ShowDialog();
    }

    private void ShowDeleteMasterViewEvent(object sender, EventArgs e)
    {
       var deleteMasterView = DeleteMasterView.GetInstance();
       var masterService = new MasterService(_connectionString);
        _ = new DeleteMasterViewPresenter(deleteMasterView, masterService);

        deleteMasterView.ShowDialog();
    }

    private async void ShowAllDataEvent(object sender, EventArgs e)
    {
        _mainView.SetAllDataToDataGridView(await _dbService.ShowAllDataAsync());
    }

    private async void RemoveData(object sender, int index)
    {
        await _customerService.DeleteCustomerByIdAsync(index);
    }

    private async void SearchCustomer(object sender, EventArgs e)
    {
        var dt = await _customerService.SearchCustomerByValue(_mainView.SearchValue);
        _mainView.SetAllDataToDataGridView(dt);
    }
}