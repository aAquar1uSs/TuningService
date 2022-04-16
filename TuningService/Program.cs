using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Configuration;
using System.Windows.Forms;
using TuningService.Services;
using TuningService.Services.Impl;
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

            var provider = GetServiceProvider();
            var mainFrom = provider.GetRequiredService<MainView>();
            Application.Run(mainFrom);
        }

        private static IServiceProvider GetServiceProvider()
        {
            var sqlConn = new NpgsqlConnection(ConfigurationManager
                .ConnectionStrings["ConnectionString"].ConnectionString);

            var serviceCollection = new ServiceCollection();

            _ = serviceCollection.AddSingleton<IDbService>(_ => new DbService(sqlConn));

            _ = serviceCollection.AddTransient<MainView>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}