using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Views
{
    public delegate Task AddNewMasterDelegate(Master master);
    public interface INewMasterView
    {
        event AddNewMasterDelegate AddNewMasterEvent;
    }
}
