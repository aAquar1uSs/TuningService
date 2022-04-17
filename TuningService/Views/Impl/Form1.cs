using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace TuningService.Views.Impl
{
    public partial class MainView : Form , IMainView
    {
        public string SearchValue
        {
            get => textBoxSearch.Text;
            set => textBoxSearch.Text = value;
        }

        public MainView()
        {
            InitializeComponent();
            WindowState = FormWindowState.Normal;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            SetEventHandlerForOrderButton();
            SetSearchEvents();
            buttonAddNewOrder.Click += (_, _) => ShowNewOrderViewEvent?.Invoke(this, EventArgs.Empty);
            buttonUpdate.Click += (_, _) => ShowAllDataEvent?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<int> ShowOrderInfoViewEvent;
        public event EventHandler ShowNewOrderViewEvent;
        public event EventHandler ShowAllDataEvent;
        public event EventHandler<int> RemoveDataFromTableEvent;
        public event EventHandler SearchEvent;


        public void SetAllDataToDataGridView(DataTable dt)
        {
            dataGridView1.DataSource = dt;
        }

        private void SetEventHandlerForOrderButton()
        {
            try
            {
                //DataGridHandler
                dataGridView1.DoubleClick += (_,_) =>
                    ShowOrderInfoViewEvent?.Invoke(this, Convert
                        .ToInt32(dataGridView1[5, dataGridView1.CurrentRow.Index]
                            .Value
                            .ToString()));
                //ButtonHandler
                buttonShowOrder.Click += (_,_) =>
                    ShowOrderInfoViewEvent?.Invoke(this, Convert
                        .ToInt32(dataGridView1[5, dataGridView1.CurrentRow.Index]
                            .Value
                            .ToString()));
            }
            catch (FormatException)
            {
                MessageBox.Show("Please select a user to view his order!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SetSearchEvents()
        {
            buttonSearch.Click += delegate
            {
                SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            textBoxSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };
        }

        private void Form1_LoadAsync(object sender, EventArgs e)
        {
            try
            {
                ShowAllDataEvent?.Invoke(this, EventArgs.Empty);

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
        }

        private void buttonAddNewOrder_Click(object sender, EventArgs e)
        {
            var orderView = new OrderView();
            orderView.ShowDialog();
        }

        private void buttonRemove_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow is null)
                    throw new FormatException();

                var customerId = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());

                var result = MessageBox.Show("Are you sure? You want to delete the data?",
                "You sure?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );

                if (result == DialogResult.No)
                    return;

                RemoveDataFromTableEvent?.Invoke(sender, customerId);
                ShowAllDataEvent?.Invoke(this, EventArgs.Empty);
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
    }
}