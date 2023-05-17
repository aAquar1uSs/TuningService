using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;
using TuningService.Models;
using TuningService.Repository;
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
    
    public NewOrderViewPresenter(
        NpgsqlConnection db,
        INewOrderView orderView,
        ICarRepository carRepository,
        ICustomerRepository customerRepository,
        IMasterRepository masterRepository,
        IOrderRepository orderRepository,
        ITuningBoxRepository tuningBoxRepository)
    {
        _db = db;
        _newOrder = orderView;
        _customerRepository = customerRepository;
        _masterRepository = masterRepository;
        _orderRepository = orderRepository;
        _carRepository = carRepository;
        _tuningBoxRepository = tuningBoxRepository;

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

        var customerId = await _customerRepository.InsertAsync(customer);

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