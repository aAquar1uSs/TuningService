using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Services
{
    public interface IDbService
    {
        Task<DataTable> ShowAllDataAsync();

        //Task<IEnumerable<TuningBox>> GetCommonData();
    }
}
