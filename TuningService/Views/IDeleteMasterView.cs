using System;
using System.Data;
using TuningService.Models;

namespace TuningService.Views
{
    public interface IDeleteMasterView
    {
        event EventHandler UpdateListOfMastersEvent;

        event EventHandler DeleteMasterEvent;

        Master MasterInfo { get; set; }

        void SetDataAboutMasters(DataTable dt);
    }
}
