using Npgsql;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        ITuningBoxService tuningBoxService)
    {
        _newOrder = orderView;
        _customerService = customerService;
        _masterService = masterService;
        _orderService = orderService;
        _carService = carService;
        _tuningBoxService = tuningBoxService;

        _newOrder.UpdateListOfMasters += UpdateMastersAsync;
        _newOrder.AddNewCustomerEvent += AddCustomerAsync;
        _newOrder.AddNewCarEvent += AddCarAsync;
        _newOrder.UproveMasterEvent += UproveMasterAsync;
        _newOrder.AddNewOrderEvent += AddOrderAsync;
        _newOrder.CreateTuningBoxEvent += CreateTuningBoxAsync;
        _newOrder.VerifyBoxNumberEvent += VerifyTuningBoxNumberAsync;
    }

    private async void UpdateMastersAsync(object sender, EventArgs e)
    {
        var dt = await _masterService.GetAllMastersAsync();
        _newOrder.SetDataAboutMasters(dt);
    }

    private async Task<int> AddCustomerAsync(Customer customer)
    {
        await _customerService.InsertNewCustomerAsync(customer);
        return await _customerService.GetCustomerIdByFullInformationAsync(customer); 
    }

    private async Task<int> AddCarAsync(Car car)
    {
        await _carService.InsertNewCarAsync(car);
        return await _carService.GetCarIdByFullInformationAsync(car);
    }

    private async Task<int> UproveMasterAsync(Master master)
    {
         return await _masterService.GetMasterIdByFullInformation(master);
    }

    private async Task<bool> VerifyTuningBoxNumberAsync(object sender, EventArgs e)
    {
        return await _tuningBoxService.VerifyBoxNumberAsync(_newOrder.BoxId);
    }

    private async Task<int> CreateTuningBoxAsync(TuningBox tuningBox, int carId)
    {
        try
        {
            await _tuningBoxService.InsertNewTuningBoxAsync(tuningBox);
            return await _tuningBoxService.GetTuningBoxIdByCarIdAsync(carId);
        }
        catch (InvalidOperationException)
        {
            MessageBox.Show("This room already taken.",
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return 0;
        }
    }

    private async Task AddOrderAsync(Order order)
    {
        await _orderService.InsertNewOrderAsync(order);
    }
}