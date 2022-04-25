using System;
using TuningService.Services;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class NewMasterViewPresenter
    {
        private readonly INewMasterView _newMasterView;

        private readonly IMasterService _masterService;

        public NewMasterViewPresenter(INewMasterView masterView,
            IMasterService masterService)
        {
            _newMasterView = masterView;
            _masterService = masterService;

            _newMasterView.AddNewMaster += AddNewMasterEvent;
        }

        private async void AddNewMasterEvent(object sender, EventArgs e)
        {
            await _masterService.InsertNewMasterAsync(_newMasterView.Master);
        }
    }
}
