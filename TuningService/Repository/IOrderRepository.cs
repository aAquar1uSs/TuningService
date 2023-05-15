using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Repository;

public interface IOrderRepository
{
    Task<Order?> GetOrderByTuningBoxIdAsync(int tuningBoxId);

    Task ChangeStateAsync(Order order);

    Task InsertAsync(Order order);

    Task<Order> GetAsync(int id);

    Task UpdateAsync(Order order);
}