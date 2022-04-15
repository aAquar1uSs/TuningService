using System.Data;

namespace TuningService.Services
{
    public interface IDbService
    {
        DataTable ShowAllData();

        void DeleteCustomerById(int customerId);

        DataTable ShowOrderByTuningBoxId(int customerId);
    }
}
