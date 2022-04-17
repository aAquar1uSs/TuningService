using System;
using TuningService.Models;

namespace TuningService.Views;

public interface IOrderInfoView
{
    event EventHandler<int> LoadFullInformationAboutOrder;

    public void ShowInformationAboutOrder(TuningBox tuningBox);
}