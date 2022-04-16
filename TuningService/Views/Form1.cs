using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using TuningService.Services;
using TuningService.Views;

namespace TuningService
{
    public partial class MainView : Form
    {
        private readonly IDbService _dbService;

        public MainView(IDbService dbService)
        {
            InitializeComponent();
            WindowState = FormWindowState.Normal;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            _dbService = dbService;
        }

        private async void Form1_LoadAsync(object sender, EventArgs e)
        {
            try
            {
                await UpdateDatabaseAsync();
                if (dataGridView1.ColumnCount < 9)
                    throw new ArgumentException();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show("The database connection failed. Check the connection and try again.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            catch (ArgumentException)
            {
                return;
            }
            InitHeadersInTable();
        }

        private async void buttonUpdate_ClickAsync(object sender, EventArgs e)
        {
            await UpdateDatabaseAsync();
        }

        private void buttonAddNewOrder_Click(object sender, EventArgs e)
        {
            var orderView = new OrderView();
            orderView.ShowDialog();
        }

        private async void buttonRemove_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                var customerId = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());

                var result = MessageBox.Show("Are you sure? You want to delete the data?",
                "You sure?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );

                if (result == DialogResult.No)
                    return;

                await _dbService.DeleteCustomerByIdAsync(customerId);
                await UpdateDatabaseAsync();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please select the line you want to delete!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void InitHeadersInTable()
        {
            dataGridView1.Columns[0].HeaderText = "Id";
            dataGridView1.Columns[1].HeaderText = "Full name";
            dataGridView1.Columns[2].HeaderText = "Customer phone";
            dataGridView1.Columns[3].HeaderText = "Car id";
            dataGridView1.Columns[4].HeaderText = "Car";
            dataGridView1.Columns[5].HeaderText = "Tuning box";
            dataGridView1.Columns[6].HeaderText = "In work";
            dataGridView1.Columns[7].HeaderText = "Master full name";
            dataGridView1.Columns[8].HeaderText = "Master phone";
        }

        private async Task UpdateDatabaseAsync()
        {
            dataGridView1.DataSource = await _dbService.ShowAllDataAsync();
        }

        private async void buttonShowOrder_ClickAsync(object sender, EventArgs e)
        {
            var orderInfoView = new OrderInfoView(_dbService);
            try
            {
                var tuningBoxId = Convert.ToInt32(dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString());
                await orderInfoView.LoadOrderAsync(tuningBoxId);
                orderInfoView.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a user to view his order!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}