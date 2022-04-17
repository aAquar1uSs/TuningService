using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Services.Impl;

public class TuningBoxService
{
    private readonly IOrderService _orderService;

    private readonly IMasterService _masterService;

    public TuningBoxService(IOrderService orderService, IMasterService masterService)
    {
        _orderService = orderService;
        _masterService = masterService;
    }

    public async Task<TuningBox> GetFulInformationAboutTuningBoxById(int tuningBoxId)
    {
        var order = await _orderService.GetOrderByTuningBoxIdAsync(tuningBoxId);
        var master = await _masterService.GetMasterByTuningBoxIdAsync(tuningBoxId);

        if (order is null || master is null)
            return null;

        return new TuningBox(tuningBoxId, master, order);
    }
}