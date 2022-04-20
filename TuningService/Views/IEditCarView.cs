using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Views
{
    public interface IEditCarView
    {
        event EventHandler<int> GetCarDataEvent;

        event EventHandler UpdateCarDataEvent;

        public Car Car { get; set; }

        public void GetCarData(int carId);

        public void ShowOldData();
    }
}
