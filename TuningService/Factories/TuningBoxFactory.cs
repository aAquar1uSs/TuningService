using TuningService.Models;

namespace TuningService.Factories;

public static class TuningBoxFactory
{
    public static TuningBox GetTuningBoxInstance(int boxNumber, Master master, Car car)
    {
        return new TuningBox
        {
            BoxNumber = boxNumber,
            Master = master,
            Car = car
        };
    }
}