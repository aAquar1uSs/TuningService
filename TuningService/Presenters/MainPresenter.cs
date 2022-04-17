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
        _mainView.ShowAllDataEvent += ShowAllDataEvent;
        _mainView.RemoveDataFromTableEvent += RemoveData;
        _mainView.SearchEvent += SearchCustomer;
    }

    private async void ShowOrderInfoViewEvent(object sender, int index)
    {
        var orderService = new OrderService(_connectionString);
        var masterService = new MasterService(_connectionString);

        var orderInfoView = OrderInfoView.GetInstance();
        _ = new OrderInfoPresenter(orderInfoView, orderService, masterService);
        await orderInfoView.LoadOrderAsync(index);
        orderInfoView.Show();
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