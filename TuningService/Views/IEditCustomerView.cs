using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuningService.Models;

namespace TuningService.Views
{
    public delegate Task UpdateCustomerDelegate(Customer customer);
    public delegate Task UploadCustomerDelegate(int customerId);
    public interface IEditCustomerView
    {
        event UploadCustomerDelegate GetCustomerDataEvent;

        event UpdateCustomerDelegate UpdateCustomerDataEvent;

        void GetDataAsync(int customerId);

        void ShowCustomerInformation(Customer customer);
    }
}
