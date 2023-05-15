using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using TuningService.Models;
using TuningService.Repository;
using TuningService.Repository.Impl;
using TuningService.Views;
using TuningService.Views.Impl.EditMenu;

namespace TuningService.Presenters;

public class OrderInfoPresenter
{
    private readonly IOrderInfoView _orderInfoView;
    private readonly TuningBoxRepository _tuningBoxRepository;
    private readonly IOrderRepository _orderRepository;
    private Order _order;

    private readonly NpgsqlConnection _db;


    public OrderInfoPresenter(IOrderInfoView orderInfoView,
        IOrderRepository orderRepository,
        TuningBoxRepository boxRepository,
        NpgsqlConnection db)
    {
        _orderInfoView = orderInfoView;
        _orderRepository = orderRepository;
        _tuningBoxRepository = boxRepository;
        _db = db;

        _orderInfoView.LoadFullInformationOrderEvent += GetFullInformationAboutOrderEvent;
        _orderInfoView.ChangeStateOrderEvent += ChangeStateOrderAsync;
        _orderInfoView.ShowEditCarEvent += ShowEditCarView;
        _orderInfoView.ShowEditCustomerEvent += ShowEditCustomerView;
        _orderInfoView.ShowEditOrderEvent += ShowEditOrderView;
    }

    private async Task GetFullInformationAboutOrderEvent(int boxId)
    {
        try
        {
            _order = await _orderRepository.GetOrderByTuningBoxIdAsync(boxId);


            if (_order is null)
            {
                MessageBox.Show("Order could not be loaded!",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }

            _order.TuningBox = await _tuningBoxRepository.GetFulInformationAboutTuningBoxById(boxId);

            if (_order.TuningBox is null)
            {
                MessageBox.Show("Order could not be loaded!",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }

            _orderInfoView.ShowInformationAboutOrder(_order);


        }
        catch (InvalidOperationException)
        {
        }
    }

    private async void ChangeStateOrderAsync(object sender, EventArgs e)
    {
        if (_order is null)
            return;

        await _orderRepository.ChangeStateAsync(_order);
        _orderInfoView.ShowInformationAboutOrder(_order);
    }

    private void ShowEditCarView(int carId)
    {
        var editCarView = new EditCarView();
        var carService = new CarRepository(_db);

        _ = new EditCarViewPresenter(editCarView, carService);
        editCarView.GetCarDataAsync(carId);
        editCarView.ShowDialog();
    }

    private void ShowEditCustomerView(int customerId)
    {
        var editCustomerView = new EditCustomerView();
        var customerService = new CustomerRepository(_db);

        _ = new EditCustomerViewPresenter(editCustomerView, customerService);
        editCustomerView.GetDataAsync(customerId);
        editCustomerView.ShowDialog();
    }

    private void ShowEditOrderView(int orderId)
    {
        var editOrderView = new EditOrderView();
        var orderService = new OrderRepository(_db);

        _ = new EditOrderViewPresenter(editOrderView, orderService);
        editOrderView.GetOrderData(orderId);
        editOrderView.ShowDialog();
    }
}