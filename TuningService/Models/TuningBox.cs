namespace TuningService.Models;

public sealed class TuningBox
{
    public int Id { get; set; }

    public Car CarInfo { get; set; }

    public Master MasterInfo { get; set; }

    public TuningBox(Master master, Car carInfo)
    {
        CarInfo = carInfo;
        MasterInfo = master;
    }
}