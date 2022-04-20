using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Views
{
    public interface IEditOrderView
    {
        event EventHandler<int> GetOrderDataEvent;

        event EventHandler UpdateOrderDataEvent;

        public void GetOldOrderData(int orderId);

        Order OrderInfo { get; set; }
        public void ShowInformation();
    }
}
