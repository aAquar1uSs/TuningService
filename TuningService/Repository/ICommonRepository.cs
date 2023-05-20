using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TuningService.Models.ViewModels;

namespace TuningService.Repository
{
    public interface ICommonRepository
    {
        Task<IReadOnlyCollection<ComparedDataView>> ShowAllDataAsync();

        Task<IReadOnlyCollection<ComparedDataView>> SearchCustomerByValueAsync(string value);

        Task Insert(IReadOnlyCollection<DataForImport> data);
    }
}
