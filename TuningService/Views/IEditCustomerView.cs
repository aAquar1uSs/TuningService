using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Views
{
    public interface IEditCustomerView
    {
        event EventHandler<int> GetCustomerDataEvent;

        event EventHandler UpdateCustomerDataEvent;

        public Customer Customer { get; set; }

        void GetData(int customerId);

        void ShowCustomerInformation();
    }
}
