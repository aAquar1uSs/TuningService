using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using TuningService.Factories;
using TuningService.Models;
using TuningService.Models.ViewModels;

namespace TuningService.Views.Impl
{
    public partial class NewOrderView : Form, INewOrderView
    {
        private static NewOrderView _newOrderView;

        private Customer _customer;
        private Order _order;
        private Car _car;
        private Master _master;
        private TuningBox _tuningBox;
        private int _boxId;

        private NewOrderView()
        {
            InitializeComponent();
        }

        public event EventHandler UpdateListOfMasters;
        public event AddNewOrderDelegate AddNewOrderEvent;

        public static NewOrderView GetInstance()
        {
            if (_newOrderView is null || _newOrderView.IsDisposed)
            {
                _newOrderView = new NewOrderView();
                _newOrderView.FormBorderStyle = FormBorderStyle.FixedDialog;
            }
            else
            {
                if (_newOrderView.WindowState == FormWindowState.Minimized)
                    _newOrderView.WindowState = FormWindowState.Normal;
            }

            return _newOrderView;
        }
        
        protected override void WndProc(ref Message m)
        {
            const int WM_CLOSE = 0x0010;

            if (m.Msg == WM_CLOSE)
            {
                Dispose();
                return;
            }

            base.WndProc(ref m);
        }
        
        public void SetDataAboutMasters(IEnumerable<MasterViewModel> masterViewModels)
        {
            if (!masterViewModels.Any())
                return;

            comboBoxMasters.Items.AddRange(masterViewModels.Select(x => x.MasterInfo).ToArray());
            
            comboBoxMasters.DisplayMember = "concat";
            comboBoxMasters.ValueMember = "concat";
        }

        private void NewOrderView_Load(object sender, EventArgs e)
        {
            UpdateListOfMasters?.Invoke(this, EventArgs.Empty);
        }

        private bool ConfigureCustomer()
        {
            var customerName = textBoxCustomerName.Text;
            var customerSurname = textBoxCustomerSurname.Text;
            var customerLastname = textBoxCustomerLastname.Text;
            var customerPhone = textBoxCutomerPhone.Text;

            try
            {
                _customer = CustomerFactory.GetCustomerInstance(customerName, customerLastname,
                    customerSurname, customerPhone);
            }
            catch (ValidationException)
            {
                MessageBox.Show("Incorrect data entered!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
            return true;
        }

        private bool ConfigureCar()
        {
            var carName = textBoxCarName.Text;
            var carModel = textBoxCarModel.Text;

            var carResult = CarFactory.GetCarInstance(carName, carModel, _customer);
            if (carResult.IsFailure)
            {
                MessageBox.Show("Incorrect data entered!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            _car = carResult.Value;

            return true;
        }

        private bool ConfigureMaster()
        {
            var masterInfo = comboBoxMasters.Text.Split(' ');
            var masterName = masterInfo[0];
            var masterSurname = masterInfo[1];

            try
            {
                _master = MasterFactory.GetMasterInstance(masterName, masterSurname);
            }
            catch (ValidationException)
            {
                MessageBox.Show("Incorrect data entered!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter the box number!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ConfigureTuningBox()
        {
            try
            {
                _boxId = Convert.ToInt32(textBoxBoxNumber.Text);
                _tuningBox = TuningBoxFactory.GetTuningBoxInstance(_boxId, _master, _car);
            }
            catch (ValidationException)
            {
                MessageBox.Show("Incorrect data entered!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter the box number!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool ConfigureOrder()
        {
            string pattern = "yyyy-mm-dd";
            try
            {
                var finishData = DateTime.ParseExact(textBoxFinishDate.Text, pattern, CultureInfo.InvariantCulture);
                var price = decimal.Parse(textBoxPrice.Text);
                var isDone = checkBoxInWork.Checked;
                var desc = richTextBoxDesc.Text;

                _order = OrderFactory.GetOrderInstance(finishData, price, isDone, desc, _tuningBox);
            }
            catch (ValidationException)
            {
                MessageBox.Show("Incorrect data entered!",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong format!!",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        

        private async void buttonAddOrder_Click(object sender, EventArgs e)
        {
            if (!ConfigureCustomer() 
                || !ConfigureCar()
                || !ConfigureMaster()
                || !ConfigureTuningBox()
                || !ConfigureOrder())
            {
                return;
            }

            await AddNewOrderEvent?.Invoke(_car, _master, _customer, _tuningBox,_order);

            var result = MessageBox.Show("New order successfully added!",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            if (result == DialogResult.OK)
                _newOrderView.Close();
        }
    }
}
