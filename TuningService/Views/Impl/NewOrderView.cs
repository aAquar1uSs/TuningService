using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using TuningService.Factories;
using TuningService.Models;

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

        public Customer Customer { get => _customer; set => _customer = value; }

        public Car Car { get => _car; set => _car = value; }

        public TuningBox TuningBox { get => _tuningBox; set => _tuningBox = value; }

        public Master Master { get => _master; set => _master = value; }

        public Order Order { get => _order; set => _order = value; }

        private NewOrderView()
        {
            InitializeComponent();
        }

        public event EventHandler UpdateListOfMasters;
        public event EventHandler AddNewCarEvent;
        public event EventHandler AddNewCustomerEvent;
        public event EventHandler AddNewOrderEvent;
        public event EventHandler UproveMasterAndCreateTuningBoxEvent;

        public static NewOrderView GetInstance()
        {
            if (_newOrderView is null || _newOrderView.IsDisposed)
            {
                _newOrderView = new NewOrderView();
                _newOrderView.FormBorderStyle = FormBorderStyle.None;
            }
            else
            {
                if (_newOrderView.WindowState == FormWindowState.Minimized)
                    _newOrderView.WindowState = FormWindowState.Normal;
            }

            return _newOrderView;
        }

        public void SetDataAboutMasters(DataTable dt)
        {
            comboBoxMasters.DataSource = dt;
            comboBoxMasters.DisplayMember = "concat";
            comboBoxMasters.ValueMember = "concat";
        }

        private void NewOrderView_Load(object sender, EventArgs e)
        {
            UpdateListOfMasters?.Invoke(this, EventArgs.Empty);
        }

        private void buttonAddCustomer_Click(object sender, EventArgs e)
        {
            //Customer
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

                return;
            }

            AddNewCustomerEvent?.Invoke(sender, EventArgs.Empty);
            buttonAddCustomer.Enabled = false;
            buttonAddCar.Enabled = true;
        }

        private void buttonAddCar_Click(object sender, EventArgs e)
        {
            var carName = textBoxCarName.Text;
            var carModel = textBoxCarModel.Text;

            try
            {
                _car = CarFactory.GetCarInstance(carName, carModel, _customer);
            }
            catch (ValidationException)
            {
                MessageBox.Show("Incorrect data entered!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            AddNewCarEvent?.Invoke(sender, EventArgs.Empty);
            buttonAddCar.Enabled = false;
            buttonAddMaster.Enabled = true;
        }

        private void buttonAddMaster_Click(object sender, EventArgs e)
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

                return;
            }

            UproveMasterAndCreateTuningBoxEvent?.Invoke(sender, EventArgs.Empty);
            buttonAddMaster.Enabled = false;
            buttonAddOrder.Enabled = true;
        }

        private void buttonAddOrder_Click(object sender, EventArgs e)
        {
            string pattern = "yyyy-mm-dd";
            try
            {
                var finishData = DateTime.ParseExact(textBoxFinishDate.Text, pattern, CultureInfo.InvariantCulture);
                var price = Decimal.Parse(textBoxPrice.Text);
                var inWork = checkBoxInWork.Checked;
                var desc = richTextBoxDesc.Text;

                _order = OrderFactory.GetOrderInstance(finishData, price, inWork, desc, _tuningBox);
            }
            catch (ValidationException)
            {
                MessageBox.Show("Incorrect data entered!",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);

                return;
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong format!!",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);

                return;
            }

            AddNewOrderEvent?.Invoke(sender, EventArgs.Empty);

            var result = MessageBox.Show("New order successfully added!",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            
            if (result == DialogResult.OK)
                _newOrderView.Close();
        }
    }
}
