using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;
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

        public bool BoxIsExist { get; set; }

        public int BoxId {get; set;}

        private NewOrderView()
        {
            InitializeComponent();
        }

        public event EventHandler UpdateListOfMasters;
        public event AddNewCarDelegate AddNewCarEvent;
        public event AddNewCustomerDelegate AddNewCustomerEvent;
        public event AddNewOrderDelegate AddNewOrderEvent;
        public event UproveMasterDelegate UproveMasterEvent;
        public event CreateTuningBoxDelegate CreateTuningBoxEvent;
        public event VerifyBoxNumberDelegate VerifyBoxNumberEvent;

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

        public void SetDataAboutMasters(DataTable dt)
        {
            if (dt.Columns.Count == 0)
                return;
            comboBoxMasters.DataSource = dt;
            comboBoxMasters.DisplayMember = "concat";
            comboBoxMasters.ValueMember = "concat";
        }

        private void NewOrderView_Load(object sender, EventArgs e)
        {
            UpdateListOfMasters?.Invoke(this, EventArgs.Empty);
        }

        private void ConfigureCustomer()
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
        }

        private void ConfigureCar()
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
        }

        private void ConfigureMaster()
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
            catch (FormatException)
            {
                MessageBox.Show("Please enter the box number!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }  
        }

        private void ConfigureTuningBox()
        {
            try
            {
                BoxId = Convert.ToInt32(textBoxBoxNumber.Text);
                _tuningBox = TuningBoxFactory.GetTuningBoxInstance(BoxId, _master, _car);
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
                MessageBox.Show("Please enter the box number!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
        }

        private void ConfigureOrder()
        {
            string pattern = "yyyy-mm-dd";
            try
            {
                var finishData = DateTime.ParseExact(textBoxFinishDate.Text, pattern, CultureInfo.InvariantCulture);
                var price = Decimal.Parse(textBoxPrice.Text);
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
        }

        private async Task VerifyBoxNumberAsync(object sender, EventArgs e)
        {
            try
            {
                BoxId = Convert.ToInt32(textBoxBoxNumber.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter the box number!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            await VerifyBoxNumberEvent?.Invoke(sender, e);
        }

        private async void buttonAddOrder_Click(object sender, EventArgs e)
        {
            await VerifyBoxNumberAsync(sender, e);

            if (BoxIsExist)
            {
                MessageBox.Show("Box has already taken!",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            ConfigureCustomer();

            ConfigureCar();

            ConfigureMaster();

            ConfigureTuningBox();

            ConfigureOrder();

            _customer.Id = await AddNewCustomerEvent?.Invoke(_customer);

            _car.Id = await AddNewCarEvent?.Invoke(_car);

            _master.Id = await UproveMasterEvent?.Invoke(_master);

            _tuningBox.Id = await CreateTuningBoxEvent?.Invoke(_tuningBox, _car.Id);

            await AddNewOrderEvent?.Invoke(_order);

            var result = MessageBox.Show("New order successfully added!",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            if (result == DialogResult.OK)
                _newOrderView.Close();
        }
    }
}
