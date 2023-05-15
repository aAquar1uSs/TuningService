using System;
using System.Configuration;
using System.Windows.Forms;
using Npgsql;
using TuningService.Presenters;
using TuningService.Repository.Impl;
using TuningService.Views.Impl;

namespace TuningService
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var sqlConnectionString = ConfigurationManager
                .ConnectionStrings["ConnectionString"].ConnectionString;

            var db = new NpgsqlConnection(sqlConnectionString);

            var dbService = new CommonService(db);
            var customerService = new CustomerRepository(db);
            var view = new MainView();

            _ = new MainPresenter(view, dbService, customerService, db);
            Application.Run(view);
        }
    }
}