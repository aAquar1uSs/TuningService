using System;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    
    public NewOrderViewPresenter(
        INewOrderView orderView,
        ICarRepository carRepository,
        ICustomerRepository customerRepository,
        IMasterRepository masterRepository,
        IOrderRepository orderRepository,
        ITuningBoxRepository tuningBoxRepository)
    {
        _newOrder = orderView;
        _customerRepository = customerRepository;
        _masterRepository = masterRepository;
        _orderRepository = orderRepository;
        _carRepository = carRepository;
        _tuningBoxRepository = tuningBoxRepository;

        _newOrder.UpdateListOfMasters += UpdateMastersAsync;
        _newOrder.AddNewCustomerEvent += AddCustomerAsync;
        _newOrder.AddNewCarEvent += AddCarAsync;
        _newOrder.UploadMasterEvent += UploadMasterAsync;
        _newOrder.AddNewOrderEvent += AddOrderAsync;
        _newOrder.CreateTuningBoxEvent += CreateTuningBoxAsync;
        _newOrder.VerifyBoxNumberEvent += VerifyTuningBoxNumberAsync;
    }

    private async void UpdateMastersAsync(object sender, EventArgs e)
    {
        var dt = await _masterRepository.GetAllAsync();
        _newOrder.SetDataAboutMasters(dt);
    }

    private async Task<int> AddCustomerAsync(Customer customer)
    {
        await _customerRepository.InsertAsync(customer);
        return await _customerRepository.GetCustomerIdByFullInformationAsync(customer);
    }

    private async Task<int> AddCarAsync(Car car)
    {
        await _carRepository.InsertAsync(car);
        return await _carRepository.GetCarIdByFullInformationAsync(car);
    }

    private async Task<int> UploadMasterAsync(Master master)
    {
         return await _masterRepository.GetMasterIdAsync(master);
    }

    private async Task<bool> VerifyTuningBoxNumberAsync(int boxId)
    {
        return await _tuningBoxRepository.VerifyBoxNumberAsync(boxId);
    }

    private async Task<int> CreateTuningBoxAsync(TuningBox newTuningBox, int carId)
    {
        var tuningBox = await _tuningBoxRepository.GetFulInformationAboutTuningBoxById(newTuningBox.BoxNumber);
        if (tuningBox is not null)
        {
            MessageBox.Show("This room already taken.",
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return 0;
        }
        await _tuningBoxRepository.InsertAsync(newTuningBox);
        
        return await _tuningBoxRepository.GetTuningBoxIdByCarIdAsync(carId);
    }

    private async Task AddOrderAsync(Order order)
    {
        await _orderRepository.InsertAsync(order);
    }
}