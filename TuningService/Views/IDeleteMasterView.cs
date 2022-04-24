using System;
using System.Data;
using System.Threading.Tasks;
using TuningService.Models;


namespace TuningService.Views
{
    public delegate Task DeleteMasterDelegate(Master master);
    public interface IDeleteMasterView
    {
        event EventHandler UpdateListOfMastersEvent;

        event DeleteMasterDelegate DeleteMasterEvent;
        void SetDataAboutMasters(DataTable dt);
    }
}
