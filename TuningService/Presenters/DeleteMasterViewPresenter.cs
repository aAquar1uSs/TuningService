using System;
using System.Threading.Tasks;
using Npgsql;
using TuningService.Models;
using TuningService.Repository;
using TuningService.Repository.Impl;
using TuningService.Utilites.Settings;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class DeleteMasterViewPresenter
    {
        private readonly IDeleteMasterView _deleteMasterView;
        private readonly IMasterRepository _masterRepository;
        private readonly ITuningBoxRepository _tuningBoxRepository;

        public DeleteMasterViewPresenter(IDeleteMasterView deleteMasterView)
        {
            _deleteMasterView = deleteMasterView;
            _masterRepository = new MasterRepository(new NpgsqlConnection(AppConnection.ConnectionString));
            _tuningBoxRepository = new TuningBoxRepository(new NpgsqlConnection(AppConnection.ConnectionString));

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
