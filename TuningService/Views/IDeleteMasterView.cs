using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TuningService.Models;
using TuningService.Models.ViewModels;


namespace TuningService.Views
{
    public delegate Task DeleteMasterDelegate(Master oldMaster, Master newMaster);
    public interface IDeleteMasterView
    {
        event EventHandler UpdateListOfMastersEvent;

        event DeleteMasterDelegate DeleteAndReplaceMasterEvent;

        void SetDataAboutMasters(IEnumerable<MasterViewModel> masterViewModels);
    }
}