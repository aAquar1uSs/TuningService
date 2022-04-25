using System.Threading.Tasks;
using System.Windows.Forms;
using TuningService.Models;
using TuningService.Services;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class NewMasterViewPresenter
    {
        private readonly IMasterService _masterService;

        public NewMasterViewPresenter(INewMasterView masterView,
            IMasterService masterService)
        {
            _masterService = masterService;

            masterView.AddNewMasterEvent += AddNewMasterEvent;
        }

        private async Task AddNewMasterEvent(Master master)
        {
            if (!await _masterService.InsertNewMasterAsync(master))
            {
                MessageBox.Show("An unexpected error has occurred!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
