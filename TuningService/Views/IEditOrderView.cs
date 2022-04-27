using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Views
{
    public delegate Task GetOrderDataDelegate(int orderId);
    public delegate Task UpdateOrderDataDelegate(Order order);

    public interface IEditOrderView
    {
        event GetOrderDataDelegate GetOrderDataEvent;

        event UpdateOrderDataDelegate UpdateOrderDataEvent;

        public void GetOldOrderData(int orderId);

        public void ShowInformation(Order order);
    }
}
