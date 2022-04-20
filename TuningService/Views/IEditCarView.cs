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
        event EventHandler<int> UploadDataEvent;

        event EventHandler ChangeCarDataEvent;

        public Car Car { get; set; }

        public void UploadCarData(int carId);

        public void ShowOldData();
    }
}
