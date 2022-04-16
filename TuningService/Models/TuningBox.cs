namespace TuningService.Models;

public sealed class TuningBox
{
    public int Id { get; set; }

    public Order OrderInfo { get; set; }

    public Master MasterInfo { get; set; }

    public bool OnWork { get; set; }

    public TuningBox(int id, Master master, Order order)
    {
        Id = order.TuningBoxId;
        OrderInfo = order;
        MasterInfo = master;
    }
}