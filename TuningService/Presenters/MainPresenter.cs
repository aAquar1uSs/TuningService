using System;
using Npgsql;
using TuningService.Repository;
using TuningService.Repository.Impl;
using TuningService.Views;
using TuningService.Views.Impl;

namespace TuningService.Presenters;

public class MainPresenter
{
    private readonly IMainView _mainView;
    private readonly ICommonService _commonService;
    private readonly ICustomerRepository _customerRepository;
    private readonly NpgsqlConnection _db;

    public MainPresenter(IMainView mainView,
        ICommonService commonService,
        ICustomerRepository customerRepository,
        NpgsqlConnection db)
    {
        _mainView = mainView;
        _commonService = commonService;
        _customerRepository = customerRepository;
        _db = db;

        _mainView.ShowOrderInfoViewEvent += ShowOrderInfoViewEvent;
        _mainView.UpdateAllDataEvent += ShowAllDataEvent;
        _mainView.RemoveDataFromTableEvent += RemoveCustomer;
        _mainView.SearchEvent += SearchCustomer;
        _mainView.ShowNewOrderViewEvent += ShowNewOrderViewEvent;
        _mainView.ShowNewMasterView += ShowNewMasterViewEvent;
        _mainView.ShowDeleteMasterView += ShowDeleteMasterViewEvent;
        _mainView.ShowImportMenuView += ShowImportMenuView; 
    }

    private void ShowOrderInfoViewEvent(int index)
    {
        var orderService = new OrderRepository(_db);
        var tuningBoxService = new TuningBoxRepository(_db);

        var orderInfoView = OrderInfoView.GetInstance();
        _ = new OrderInfoPresenter(orderInfoView, orderService, tuningBoxService, _db);
        orderInfoView.LoadOrderAsync(index);
        orderInfoView.Show();
    }

    private void ShowNewOrderViewEvent(object sender, EventArgs e)
    {
        var newOrderView = NewOrderView.GetInstance();

        var orderService = new OrderRepository(_db);
        var masterService = new MasterRepository(_db);
        var carService = new CarRepository(_db);
        var tuningBoxService = new TuningBoxRepository(_db);

        _ = new NewOrderViewPresenter(newOrderView,carService, _customerRepository, masterService, orderService, tuningBoxService);

        newOrderView.ShowDialog();
    }

    private void ShowNewMasterViewEvent(object sender, EventArgs e)
    {
        var newMasterView = NewMasterView.GetInstance();
        var masterService = new MasterRepository(_db);
        _ = new NewMasterViewPresenter(newMasterView, masterService);

        newMasterView.ShowDialog();
    }

    private void ShowDeleteMasterViewEvent(object sender, EventArgs e)
    {
        var deleteMasterView = DeleteMasterView.GetInstance();
        var masterService = new MasterRepository(_db);
        var tuningBoxService = new TuningBoxRepository(_db);
        _ = new DeleteMasterViewPresenter(deleteMasterView, tuningBoxService, masterService);

        deleteMasterView.ShowDialog();
    }

    private async void ShowAllDataEvent(object sender, EventArgs e)
    {
        var compareData = await _commonService.ShowAllDataAsync();
        _mainView.SetAllDataToDataGridView(compareData);
    }

    private async void RemoveCustomer(int index)
    {
        await _customerRepository.DeleteAsync(index);
    }

    private async void SearchCustomer(object sender, EventArgs e)
    {
        var dt = await _commonService.SearchCustomerByValueAsync(_mainView.SearchValue);
        _mainView.SetAllDataToDataGridView(dt);
    }

    private void ShowImportMenuView(object sender, EventArgs e)
    {
        var importMenuView = ImportMenuView.GetInstance();
        _ = new ImportViewPresenter(importMenuView, _commonService);
        
        importMenuView.ShowDialog();
    }
}