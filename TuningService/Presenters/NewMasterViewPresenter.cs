using System.Threading.Tasks;
using Npgsql;
using TuningService.Models;
using TuningService.Repository;
using TuningService.Repository.Impl;
using TuningService.Utilites.Settings;
using TuningService.Views;

namespace TuningService.Presenters
{
    public class NewMasterViewPresenter
    {
        private readonly IMasterRepository _masterRepository;

        public NewMasterViewPresenter(INewMasterView masterView)
        {
            _masterRepository = new MasterRepository(new NpgsqlConnection(AppConnection.ConnectionString));

            masterView.AddNewMasterEvent += AddNewMasterEvent;
        }

        private async Task AddNewMasterEvent(Master master)
        {
            await _masterRepository.InsertAsync(master);
        }
    }
}
