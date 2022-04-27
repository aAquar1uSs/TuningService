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

            //SetEventHandlerForOrderButton();
            SetSearchEvents();
            buttonDeleteMaster.Click += (_, _) => ShowDeleteMasterView?.Invoke(this, EventArgs.Empty);
            buttonAddNewOrder.Click += (_, _) => ShowNewOrderViewEvent?.Invoke(this, EventArgs.Empty);
            buttonUpdate.Click += (_, _) => UpdateAllDataEvent?.Invoke(this, EventArgs.Empty);
            buttonAddMaster.Click += (_, _) => ShowNewMasterView?.Invoke(this, EventArgs.Empty);
        }

        public event ShowOrderDelegate ShowOrderInfoViewEvent;
        public event EventHandler ShowNewOrderViewEvent;
        public event EventHandler UpdateAllDataEvent;
        public event RemoveOrderDelegate RemoveDataFromTableEvent;
        public event EventHandler SearchEvent;
        public event EventHandler ShowNewMasterView;
        public event EventHandler ShowDeleteMasterView;

        public void SetAllDataToDataGridView(DataTable dt)
        {
            dataGridView1.DataSource = dt;
            if (dataGridView1.Columns.Count == 8)
                InitHeadersInTable();
        }

        private void SetSearchEvents()
        {
            buttonSearch.Click += delegate
            {
                SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            textBoxSearch.KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };
        }

        private void Form1_LoadAsync(object sender, EventArgs e)
        {
            try
            {
                UpdateAllDataEvent?.Invoke(this, EventArgs.Empty);

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
            }
            catch (ArgumentException)
            {
            }
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

                RemoveDataFromTableEvent?.Invoke(customerId);
                UpdateAllDataEvent?.Invoke(this, EventArgs.Empty);
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
            dataGridView1.Columns[6].HeaderText = "Master full name";
            dataGridView1.Columns[7].HeaderText = "Master phone";
        }

        private void buttonShowOrder_Click(object sender, EventArgs e)
        {

            try
            {
                var index = Convert
                        .ToInt32(dataGridView1[5, dataGridView1.CurrentRow.Index]
                            .Value
                            .ToString());

                //ButtonHandler
                 ShowOrderInfoViewEvent?.Invoke(index);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please select a user to view his order!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select a user to view his order!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                var index = Convert
                        .ToInt32(dataGridView1[5, dataGridView1.CurrentRow.Index]
                            .Value
                            .ToString());

                //DataGridHandler
                    ShowOrderInfoViewEvent?.Invoke(index);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please select a user to view his order!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select a user to view his order!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}