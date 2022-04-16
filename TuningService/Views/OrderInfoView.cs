using System;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using TuningService.Models;
using TuningService.Services;

namespace TuningService.Views
{
    public partial class OrderInfoView : Form
    {
        private readonly IDbService _dbService;

        public OrderInfoView(IDbService dbService)
        {
            InitializeComponent();

            _dbService = dbService;
        }

        private void OrderInfoView_Load(object sender, EventArgs e)
        {

        }

        public async Task LoadOrderAsync(int tuningBoxId)
        {
            try
            {
                var tuningBox = await _dbService.GetFulInformationAboutTuningBoxById(tuningBoxId);
                ShowInformationAboutOrder(tuningBox);
            }
            catch (NpgsqlException)
            {
                MessageBox.Show("Could not access the order table!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ShowInformationAboutOrder(TuningBox tuningBox)
        {
            if (tuningBox is null)
            {
                labelError.ForeColor = Color.Red;
                labelError.Text = "There is no record of this user’s order in the database yet!";
            }
            //Information about order
            labelOrderId.Text = tuningBox.OrderInfo.Id.ToString();
            labelStartDate.Text = tuningBox.OrderInfo.StartDate.ToString(CultureInfo.InvariantCulture);
            labelEndDate.Text = tuningBox.OrderInfo.EndDate.ToString(CultureInfo.InvariantCulture);
            orderDescription.Text = tuningBox.OrderInfo.Description;
            labelPrice.Text = tuningBox.OrderInfo.Price.ToString(CultureInfo.InvariantCulture);
            labelBoxId.Text = tuningBox.OrderInfo.TuningBoxId.ToString();
            checkBoxInWork.Checked = tuningBox.OrderInfo.InWork;

            //Information about master
            labelMasterName.Text = tuningBox.MasterInfo.Name;
            labelMasterSurname.Text = tuningBox.MasterInfo.Surname;
            labelMasterPhone.Text = tuningBox.MasterInfo.Phome;
        }
    }
}
