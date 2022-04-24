using Npgsql;
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
        private async Task DeleteMasterAsync(Master master)
        {
            if (!await _masterService.DeleteMasterByFullInfo(master))
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
