using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Services;

public interface IOrderService
{
    Task<Order> GetOrderByTuningBoxIdAsync(int tuningBoxId);

    Task ChangeStateOrderByInstance(Order order);

    Task InsertNewOrderAsync(Order order);

    Task<Order> GetOrderByIdAsync(int id);

    Task<bool> UpdateOrderDataByFullInfo(Order order);
}