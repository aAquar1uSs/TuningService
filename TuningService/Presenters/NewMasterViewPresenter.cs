using System.Threading.Tasks;
using System.Windows.Forms;
using TuningService.Models;
using TuningService.Repository;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class NewMasterViewPresenter
    {
        private readonly IMasterRepository _masterRepository;

        public NewMasterViewPresenter(INewMasterView masterView, IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;

            masterView.AddNewMasterEvent += AddNewMasterEvent;
        }

        private async Task AddNewMasterEvent(Master master)
        {
            await _masterRepository.InsertAsync(master);
        }
    }
}
