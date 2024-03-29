using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;
using TuningService.Models;
using TuningService.Repository;
using TuningService.Repository.Impl;
using TuningService.Utilites.Settings;
using TuningService.Views;

namespace TuningService.Presenters;

public class NewOrderViewPresenter
{
    private readonly INewOrderView _newOrder;
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IMasterRepository _masterRepository;
    private readonly ICarRepository _carRepository;
    private readonly ITuningBoxRepository _tuningBoxRepository;
    private readonly NpgsqlConnection _db;
    
    public NewOrderViewPresenter(INewOrderView orderView)
    {
        _db = new NpgsqlConnection(AppConnection.ConnectionString);
        _newOrder = orderView;
        _customerRepository = new CustomerRepository(_db);
        _masterRepository = new MasterRepository(_db);
        _orderRepository = new OrderRepository(_db);
        _carRepository = new CarRepository(_db);
        _tuningBoxRepository = new TuningBoxRepository(_db);

        _newOrder.UpdateListOfMasters += UpdateMastersAsync;
        _newOrder.AddNewOrderEvent += AddOrderAsync;
    }

    private async void UpdateMastersAsync(object sender, EventArgs e)
    {
        var masterViewModels = await _masterRepository.GetAllAsync();
        _newOrder.SetDataAboutMasters(masterViewModels);
    }
    
    private async Task AddOrderAsync(Car car, Master master, Customer customer, TuningBox tuningBox, Order order)
    {
        await using var transaction = _db.BeginTransaction(IsolationLevel.ReadCommitted);

        int customerId;
        var maybeCustomer = await _customerRepository.GetAsync(customer.Phone);
        if (maybeCustomer is not null)
        {
            customerId = maybeCustomer.CustomerId;
        }
        else
        {
            customerId = await _customerRepository.InsertAsync(customer);  
        }
        

        car.Owner.CustomerId = customerId;
        var carId = await _carRepository.InsertAsync(car);

        var masterId = await _masterRepository.GetMasterIdAsync(master);
        
        tuningBox.Car.CarId = carId;
        tuningBox.Master.MasterId = masterId;
        var boxId = await _tuningBoxRepository.InsertAsync(tuningBox);

        order.TuningBox.BoxId = boxId;
        await _orderRepository.InsertAsync(order);
        
        await transaction.CommitAsync();
    }
}