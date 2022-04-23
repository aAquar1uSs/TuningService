using System;
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

            _deleteMasterView.DeleteMasterEvent += DeleteMasterAsync;
            _deleteMasterView.UpdateListOfMastersEvent += UpdateDataAboutMastersAsync;
        }
        private async void DeleteMasterAsync(object sender, EventArgs e)
        {
            await _masterService.DeleteMasterByFullInfo(_deleteMasterView.MasterInfo);
        }
        private async void UpdateDataAboutMastersAsync(object sender, EventArgs e)
        {
            var dt = await _masterService.GetAllMastersAsync();
            _deleteMasterView.SetDataAboutMasters(dt);
        }
    }
}
