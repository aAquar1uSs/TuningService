using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Services;

public interface IOrderService
{
    Task<Order> GetOrderByTuningBoxIdAsync(int tuningBoxId);
    
}