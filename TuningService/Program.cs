using System;
using System.Configuration;
using System.Windows.Forms;
using TuningService.Presenters;
using TuningService.Services;
using TuningService.Services.Impl;
using TuningService.Views;
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

            IDbService dbService = new DbService(sqlConnectionString);
            ICustomerService customerService = new CustomerService(sqlConnectionString);
            IMainView view = new MainView();

            _ = new MainPresenter(view, sqlConnectionString, dbService, customerService);
            Application.Run((Form) view);
        }
    }
}