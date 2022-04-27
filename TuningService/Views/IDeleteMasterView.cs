using System;
using System.Data;
using System.Threading.Tasks;
using TuningService.Models;


namespace TuningService.Views
{
    public delegate Task DeleteMasterDelegate(Master oldMaster, Master newMaster);
    public interface IDeleteMasterView
    {
        event EventHandler UpdateListOfMastersEvent;

        event DeleteMasterDelegate DeleteAndReplaceMasterEvent;

        void SetDataAboutMasters(DataTable dt);
    }
}