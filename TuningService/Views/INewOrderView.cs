using System;
using System.Data;
using TuningService.Models;

namespace TuningService.Views;

public interface INewOrderView
{
    event EventHandler UpdateListOfMasters;

    event EventHandler AddNewCarEvent;

    event EventHandler AddNewCustomerEvent;

    event EventHandler AddNewOrderEvent;

    event EventHandler UproveMasterAndCreateTuningBoxEvent;

    public Customer Customer { get; set; }

    public Car Car { get; set; }

    public TuningBox TuningBox { get; set; }

    public Master Master { get; set; }

    public Order Order { get; set; }

    public void SetDataAboutMasters(DataTable dt);
}