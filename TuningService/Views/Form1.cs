using System;
using System.Windows.Forms;
using TuningService.Services;
using TuningService.Views;

namespace TuningService
{
    public partial class MainView : Form
    {
        private readonly IDbService _dbService;

        private readonly OrderView _orderView;

        private readonly OrderInfoView _orderInfoView;

        public MainView(OrderView orderView,
            OrderInfoView orderInfoView,
            IDbService dbService)
        {
            InitializeComponent();
            WindowState = FormWindowState.Normal;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            _dbService = dbService;
            _orderView = orderView;
            _orderInfoView = orderInfoView;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                FillingTable();
                if (dataGridView1.ColumnCount < 9)
                    throw new Exception();
            }
            catch (Exception)
            {
                MessageBox.Show("The database connection failed. Check the connection and try again.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                    );
                return;
            }   

           InitHeadersInTable();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            FillingTable();
        }

        private void buttonAddNewOrder_Click(object sender, EventArgs e)
        {
            _orderView.ShowDialog();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
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
                
                _dbService.DeleteCustomerById(customerId);
                FillingTable();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please select the line you want to delete!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
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

        private void FillingTable()
        {
            dataGridView1.DataSource = _dbService.ShowAllData();
        }

        private void buttonShowOrder_Click(object sender, EventArgs e)
        {

            try
            {
                var tuningBoxId = Convert.ToInt32(dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString());
                _orderInfoView.LoadOrder(tuningBoxId);
                _orderInfoView.Show();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please select a user to view his order!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
        }
    }
}