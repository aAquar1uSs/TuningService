using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using TuningService.Tools;

namespace TuningService
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static IServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            

            return serviceCollection.BuildServiceProvider();
        }
    }
}