using System;
using TuningService.Models;

namespace TuningService.Views;

public interface IOrderInfoView
{
    event EventHandler<int> LoadFullInformationAboutOrder;

    event EventHandler ChangeStateOrderEvent;

    public void ShowInformationAboutOrder(Order order);
}