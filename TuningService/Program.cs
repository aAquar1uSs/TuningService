using System;
using System.Configuration;
using System.Windows.Forms;
using TuningService.Presenters;
using TuningService.Services.Impl;
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

            var dbService = new DbService(sqlConnectionString);
            var customerService = new CustomerService(sqlConnectionString);
            var view = new MainView();

            _ = new MainPresenter(view, sqlConnectionString, dbService, customerService);
            Application.Run((Form) view);
        }
    }
}