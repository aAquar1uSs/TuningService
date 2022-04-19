using System;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using TuningService.Models;


namespace TuningService.Views.Impl
{
    public partial class OrderInfoView : Form, IOrderInfoView
    {
        private static OrderInfoView _orderInfoViewInstance;

        private OrderInfoView()
        {
            InitializeComponent();
        }

        public event EventHandler<int> LoadFullInformationAboutOrder;
        public event EventHandler ChangeStateOrderEvent;

        public static OrderInfoView GetInstance()
        {
            if (_orderInfoViewInstance is null || _orderInfoViewInstance.IsDisposed)
            {
                _orderInfoViewInstance = new OrderInfoView();
            }
            else
            {
                if (_orderInfoViewInstance.WindowState == FormWindowState.Minimized)
                    _orderInfoViewInstance.WindowState = FormWindowState.Normal;
            }

            return _orderInfoViewInstance;
        }

        private void OrderInfoView_Load(object sender, EventArgs e)
        {
            buttonChange.Click += ChangeStateOrderEvent;
        }

        public void LoadOrderAsync(int tuningBoxId)
        {
            try
            {
                LoadFullInformationAboutOrder?.Invoke(this, tuningBoxId);
            }
            catch (NpgsqlException)
            {
                MessageBox.Show("Could not access the order table!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void ShowInformationAboutOrder(Order order)
        {
            if (order is null)
            {
                MessageBox.Show("There is no record of this user’s order in the database yet!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            //Information about order
            labelOrderId.Text = order.Id.ToString();
            labelStartDate.Text = order.StartDate.ToString(CultureInfo.InvariantCulture);
            labelEndDate.Text = order.EndDate.ToString(CultureInfo.InvariantCulture);
            orderDescription.Text = order.Description;
            labelPrice.Text = order.Price.ToString(CultureInfo.InvariantCulture);
            labelBoxId.Text = order.TuningBox.Id.ToString();
            checkBoxInWork.Checked = order.InWork;
            checkBoxInWork.AutoCheck = false;

            //Information about master
            labelMasterName.Text = order.TuningBox.MasterInfo.Name;
            labelMasterSurname.Text = order.TuningBox.MasterInfo.Surname;
            labelMasterPhone.Text = order.TuningBox.MasterInfo.Phone;

            //Information about car
            labelCarId.Text = order.TuningBox.CarInfo.Id.ToString();
            lableCarName.Text = order.TuningBox.CarInfo.Name;
            labelCarModel.Text = order.TuningBox.CarInfo.Model;

            //Information about customer
            labelCustomerId.Text = order.TuningBox.CarInfo.Owner.Id.ToString();
            labelCustomerName.Text = order.TuningBox.CarInfo.Owner.Name;
            labelCustomerSurname.Text = order.TuningBox.CarInfo.Owner.Surname;
            labelCustomerLastname.Text = order.TuningBox.CarInfo.Owner.Lastname;
            labelCustomerPhone.Text = order.TuningBox.CarInfo.Owner.Phone;
        }
    }
}
