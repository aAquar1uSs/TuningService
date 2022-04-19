using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuningService.Services;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class NewMasterViewPresenter
    {
        private INewMasterView _newMasterView;

        private IMasterService _masterService;

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
