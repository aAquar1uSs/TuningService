using System;
using System.Threading.Tasks;
using TuningService.Models;
using TuningService.Repository;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class DeleteMasterViewPresenter
    {
        private readonly IDeleteMasterView _deleteMasterView;
        private readonly IMasterRepository _masterRepository;
        private readonly ITuningBoxRepository _tuningBoxRepository;

        public DeleteMasterViewPresenter(IDeleteMasterView deleteMasterView,
            ITuningBoxRepository tuningBoxRepository,
            IMasterRepository masterRepository)
        {
            _deleteMasterView = deleteMasterView;
            _masterRepository = masterRepository;
            _tuningBoxRepository = tuningBoxRepository;

            _deleteMasterView.DeleteAndReplaceMasterEvent += DeleteMasterAsync;
            _deleteMasterView.UpdateListOfMastersEvent += UpdateDataAboutMastersAsync;
        }
        private async Task DeleteMasterAsync(Master oldMaster, Master newMaster)
        {
            var oldId = await _masterRepository.GetMasterIdAsync(oldMaster);
            var newId = await _masterRepository.GetMasterIdAsync(newMaster);
            
            await _tuningBoxRepository.UpdateMasterIdAsync(oldId, newId);
            
            await _masterRepository.DeleteAsync(oldMaster);
        }
        private async void UpdateDataAboutMastersAsync(object sender, EventArgs e)
        {
            var masterViewModels = await _masterRepository.GetAllAsync();
            _deleteMasterView.SetDataAboutMasters(masterViewModels);
        }
    }
}
