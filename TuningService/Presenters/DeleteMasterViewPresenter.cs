using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuningService.Models;
using TuningService.Services;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class DeleteMasterViewPresenter
    {
        private readonly IDeleteMasterView _deleteMasterView;

        private readonly IMasterService _masterService;

        private readonly ITuningBoxService _tuningBoxService;

        public DeleteMasterViewPresenter(IDeleteMasterView deleteMasterView,
            ITuningBoxService tuningBoxService,
            IMasterService masterService)
        {
            _deleteMasterView = deleteMasterView;
            _masterService = masterService;
            _tuningBoxService = tuningBoxService;

            _deleteMasterView.DeleteAndReplaceMasterEvent += DeleteMasterAsync;
            _deleteMasterView.UpdateListOfMastersEvent += UpdateDataAboutMastersAsync;
        }
        private async Task DeleteMasterAsync(Master oldMaster, Master newMaster)
        {
            var oldId = await _masterService.GetMasterIdByFullInformation(oldMaster);
            var newId = await _masterService.GetMasterIdByFullInformation(newMaster);

            if (!await _tuningBoxService.UpdateMasterIdAsync(oldId, newId))
            {
                MessageBox.Show("An unexpected error has occurred!",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
            }

            if (!await _masterService.DeleteMasterByFullInfo(oldMaster))
            {
                MessageBox.Show("An unexpected error has occurred!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private async void UpdateDataAboutMastersAsync(object sender, EventArgs e)
        {
            var dt = await _masterService.GetAllMastersAsync();
            _deleteMasterView.SetDataAboutMasters(dt);
        }
    }
}
