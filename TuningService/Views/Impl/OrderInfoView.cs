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

        private void OrderInfoView_Load(object sender, EventArgs e)
        {

        }

        public async Task LoadOrderAsync(int tuningBoxId)
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

        public void ShowInformationAboutOrder(TuningBox tuningBox)
        {
            if (tuningBox is null)
            {
                labelError.ForeColor = Color.Red;
                labelError.Text = "There is no record of this user’s order in the database yet!";
                return;
            }
            //Information about order
            labelOrderId.Text = tuningBox.OrderInfo.Id.ToString();
            labelStartDate.Text = tuningBox.OrderInfo.StartDate.ToString(CultureInfo.InvariantCulture);
            labelEndDate.Text = tuningBox.OrderInfo.EndDate.ToString(CultureInfo.InvariantCulture);
            orderDescription.Text = tuningBox.OrderInfo.Description;
            labelPrice.Text = tuningBox.OrderInfo.Price.ToString(CultureInfo.InvariantCulture);
            labelBoxId.Text = tuningBox.OrderInfo.TuningBoxId.ToString();
            checkBoxInWork.Checked = tuningBox.OrderInfo.InWork;
            checkBoxInWork.AutoCheck = false;

            //Information about master
            labelMasterName.Text = tuningBox.MasterInfo.Name;
            labelMasterSurname.Text = tuningBox.MasterInfo.Surname;
            labelMasterPhone.Text = tuningBox.MasterInfo.Phone;
        }

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
    }
}
