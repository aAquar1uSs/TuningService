using System;
using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Views;

public delegate Task LoadFullInformationAboutOrderDelegate(int boxId);

public delegate void ShowEditCarDelegate(int carId);
public delegate void ShowEditCustomerDelegate(int customerId);
public delegate void ShowEditOrderDelegate(int orderId);

public interface IOrderInfoView
{
    event LoadFullInformationAboutOrderDelegate LoadFullInformationOrderEvent;

    event ShowEditCarDelegate ShowEditCarEvent;

    event ShowEditCustomerDelegate ShowEditCustomerEvent;

    event ShowEditOrderDelegate ShowEditOrderEvent;

    public void ShowInformationAboutOrder(Order order);
}