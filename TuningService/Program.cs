using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Configuration;
using System.Windows.Forms;
using TuningService.Services;
using TuningService.Views;

namespace TuningService
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var providerr = GetServiceProvider();
            var mainFrom = providerr.GetRequiredService<MainView>();
            Application.Run(mainFrom);
        }

        private static IServiceProvider GetServiceProvider()
        {
            var sqlConn = new NpgsqlConnection(ConfigurationManager
                .ConnectionStrings["ConnectionString"].ConnectionString);

            var serviceCollection = new ServiceCollection();

            _ = serviceCollection.AddSingleton<IDbService>(_ => new DbService(sqlConn));

            _ = serviceCollection.AddTransient<MainView>();
            _ = serviceCollection.AddTransient<OrderView>();
            _ = serviceCollection.AddTransient<OrderInfoView>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}