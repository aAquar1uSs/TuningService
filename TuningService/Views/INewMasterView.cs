using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Views
{
    public interface INewMasterView
    {
        event EventHandler AddNewMaster;

        Master Master { get; set; }
    }
}
