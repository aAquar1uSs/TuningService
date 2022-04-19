using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuningService.Services;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class DeleteMasterViewPresenter
    {
        private IDeleteMasterView _deleteMasterView;

        private IMasterService _masterService;

        public DeleteMasterViewPresenter(IDeleteMasterView deleteMasterView,
            IMasterService masterService)
        {
            _deleteMasterView = deleteMasterView;
            _masterService = masterService;

            _deleteMasterView.DeleteMasterEvent += DeleteMasterEvent;
            _deleteMasterView.UpdateListOfMastersEvent += UpdateDataAboutMastersEventAsync;
        }
        private async void DeleteMasterEvent(object sender, EventArgs e)
        {
            await _masterService.DeleteMasterByFullInfo(_deleteMasterView.MasterInfo);
        }
        private async void UpdateDataAboutMastersEventAsync(object sender, EventArgs e)
        {
            var dt = await _masterService.GetAllMastersAsync();
            _deleteMasterView.SetDataAboutMasters(dt);
        }
    }
}
