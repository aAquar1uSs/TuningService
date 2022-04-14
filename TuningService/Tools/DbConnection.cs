using Npgsql;
using System.Configuration;

namespace TuningService.Tools
{
    internal class DbConnection
    {
        private static DbConnection _instance;

        public NpgsqlConnection Connection { get; private set; }

        private DbConnection()
        {
            Connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        public static DbConnection GetInstance()
        {
            if (_instance == null)
                _instance = new DbConnection();
            return _instance;
        }
    }
}
