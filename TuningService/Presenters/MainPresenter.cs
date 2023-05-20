using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using TuningService.Models.ViewModels;
using TuningService.Repository;
using TuningService.Repository.Impl;
using TuningService.Utilites.Settings;
using TuningService.Views;
using TuningService.Views.Impl;

namespace TuningService.Presenters;

public class MainPresenter
{
    private readonly IMainView _mainView;
    private readonly ICommonRepository _commonRepository;
    private readonly ICustomerRepository _customerRepository;

    public MainPresenter(IMainView mainView)
    {
        _mainView = mainView;
        _commonRepository = new CommonRepository(new NpgsqlConnection(AppConnection.ConnectionString));
        _customerRepository = new CustomerRepository(new NpgsqlConnection(AppConnection.ConnectionString));

        _mainView.ShowOrderInfoViewEvent += ShowOrderInfoViewEvent;
        _mainView.UpdateAllDataEvent += ShowAllDataEvent;
        _mainView.RemoveDataFromTableEvent += RemoveCustomer;
        _mainView.SearchEvent += SearchCustomer;
        _mainView.ShowNewOrderViewEvent += ShowNewOrderViewEvent;
        _mainView.ShowNewMasterView += ShowNewMasterViewEvent;
        _mainView.ShowDeleteMasterView += ShowDeleteMasterViewEvent;
        _mainView.ShowImportMenuView += ShowImportMenuView;
        _mainView.GetDataForExport += GetDataForExport;
    }

    private void ShowOrderInfoViewEvent(int index)
    {
        var orderInfoView = OrderInfoView.GetInstance();
        _ = new OrderInfoPresenter(orderInfoView);
        orderInfoView.LoadOrderAsync(index);
        orderInfoView.Show();
    }

    private void ShowNewOrderViewEvent(object sender, EventArgs e)
    {
        var newOrderView = NewOrderView.GetInstance();
        _ = new NewOrderViewPresenter(newOrderView);

        newOrderView.ShowDialog();
    }

    private void ShowNewMasterViewEvent(object sender, EventArgs e)
    {
        var newMasterView = NewMasterView.GetInstance();
        _ = new NewMasterViewPresenter(newMasterView);

        newMasterView.ShowDialog();
    }

    private void ShowDeleteMasterViewEvent(object sender, EventArgs e)
    {
        var deleteMasterView = DeleteMasterView.GetInstance();
        _ = new DeleteMasterViewPresenter(deleteMasterView);

        deleteMasterView.ShowDialog();
    }

    private async void ShowAllDataEvent(object sender, EventArgs e)
    {
        var compareData = await _commonRepository.ShowAllDataAsync();
        
        _mainView.SetAllDataToDataGridView(compareData);
    }

    private async void RemoveCustomer(int index)
    {
        await _customerRepository.DeleteAsync(index);
    }

    private async void SearchCustomer(object sender, EventArgs e)
    {
        var comparedDataViews = await _commonRepository.SearchCustomerByValueAsync(_mainView.SearchValue);
        _mainView.SetAllDataToDataGridView(comparedDataViews);
    }

    private void ShowImportMenuView(object sender, EventArgs e)
    {
        var importMenuView = ImportMenuView.GetInstance();
        _ = new ImportViewPresenter(importMenuView);
        
        importMenuView.ShowDialog();
    }

    private async Task<IReadOnlyCollection<DataForProcessing>> GetDataForExport()
    {
        var results = await _commonRepository.GetAllDataForProcessing();

        return results;
    }
}