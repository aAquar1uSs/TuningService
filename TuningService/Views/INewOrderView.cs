using System;
using System.Data;
using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Views;

public delegate Task<bool> VerifyBoxNumberDelegate(int boxId);
public delegate Task<int> AddNewCarDelegate(Car car);
public delegate Task<int> AddNewCustomerDelegate(Customer customer);
public delegate Task<int> UploadMasterDelegate(Master master);
public delegate Task<int> CreateTuningBoxDelegate(TuningBox tuningBox, int carId);
public delegate Task AddNewOrderDelegate(Order order);

public interface INewOrderView
{
    event EventHandler UpdateListOfMasters;

    event AddNewCarDelegate AddNewCarEvent;

    event AddNewCustomerDelegate AddNewCustomerEvent;

    event AddNewOrderDelegate AddNewOrderEvent;

    event UploadMasterDelegate UploadMasterEvent;

    event CreateTuningBoxDelegate CreateTuningBoxEvent;

    event VerifyBoxNumberDelegate VerifyBoxNumberEvent;

    public void SetDataAboutMasters(DataTable dt);
}