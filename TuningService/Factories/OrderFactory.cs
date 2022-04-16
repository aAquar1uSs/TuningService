using Npgsql;
using TuningService.Models;

namespace TuningService.Factories
{
    public static class OrderFactory
    {
        public static Order GetOrderInstance(NpgsqlDataReader reader)
        {
            var id = reader.GetInt32(0);
            var startDate = reader.GetDateTime(1);
            var endDate = reader.GetDateTime(2);
            var description = reader.GetString(3);
            var price = reader.GetDecimal(4);
            var tuningBoxId = reader.GetInt32(5);
            var inWork = reader.GetBoolean(6);

            return new Order(id, startDate, endDate, description, price, tuningBoxId, inWork);
        }
    }
}
