using Npgsql;
using TuningService.Models;

namespace TuningService.Factories
{
    public static class OrderFactory
    {
        public static Order GetOrderInstance(NpgsqlDataReader reader)
        {
            var id = reader.GetInt32(0);//order_1
            var startDate = reader.GetDateTime(1);//start_date
            var endDate = reader.GetDateTime(2);//end_date
            var description = reader.GetString(3);//description
            var price = reader.GetDecimal(4);//price
            var tuningBoxId = reader.GetInt32(5);//tuning_box_id

            return new Order(id, startDate, endDate, description, price, tuningBoxId);
        } 
    }
}
