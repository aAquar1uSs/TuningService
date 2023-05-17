using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TuningService.Models;
using TuningService.Models.ViewModels;

namespace TuningService.Views;

public delegate Task AddNewOrderDelegate(Car car, Master master, Customer customer, TuningBox tuningBox, Order order);

public interface INewOrderView
{
    event EventHandler UpdateListOfMasters;

    event AddNewOrderDelegate AddNewOrderEvent;
    
    public void SetDataAboutMasters(IEnumerable<MasterViewModel> masterViewModels);
}