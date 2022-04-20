using System;
using TuningService.Models;

namespace TuningService.Views;

public interface IOrderInfoView
{
    event EventHandler LoadFullInformationAboutOrder;

    event EventHandler ChangeStateOrderEvent;

    event EventHandler<int> ShowEditCarEvent;

    public int TuningBoxId { get; set; }

    public void ShowInformationAboutOrder(Order order);
}