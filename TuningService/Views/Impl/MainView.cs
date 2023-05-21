using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using TuningService.Models.ExportModel;
using TuningService.Models.ViewModels;

namespace TuningService.Views.Impl
{
    public partial class MainView : Form , IMainView
    {
        public string SearchValue
        {
            get => textBoxSearch.Text;
            set => textBoxSearch.Text = value;
        }

        private ExportModel _exportModel { get; set; }

        public MainView()
        {
            InitializeComponent();
            WindowState = FormWindowState.Normal;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            
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
        public event EventHandler ShowImportMenuView;
        public event Func<Task<IReadOnlyCollection<DataForProcessing>>> GetDataForExport; 


        public void SetAllDataToDataGridView(IReadOnlyCollection<ComparedDataView> comparedDataViews)
        {
            var dataTable = new DataTable();
            var properties = typeof(ComparedDataView).GetProperties();

            foreach (var property in properties)
            {
                dataTable.Columns.Add(property.Name, property.PropertyType);
            }
            
            foreach (var comparedData in comparedDataViews)
            {
                dataTable.LoadDataRow(new object[]
                {
                    comparedData.CustomerId,
                    comparedData.CustomerName,
                    comparedData.CustomerPhone,
                    comparedData.CarId,
                    comparedData.CarModel,
                    comparedData.BoxId,
                    comparedData.MasterName,
                    comparedData.MasterPhone
                }, true);
            }
            
            dataGridView1.DataSource = dataTable;
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
            }
            catch (FormatException)
            {
                MessageBox.Show("Please select the line you want to delete!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void buttonShowOrder_Click(object sender, EventArgs e)
        {
            try
            {
                var index = Convert
                        .ToInt32(dataGridView1[5, dataGridView1.CurrentRow.Index]
                            .Value
                            .ToString());
                
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

        private async void ExportToCSV(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
                return;

            using var sfd = new SaveFileDialog { Filter = "CSV files (*.csv)|*.csv", ValidateNames = true };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                _exportModel = new ExportModel
                {
                    Data = (await GetDataForExport?.Invoke()).ToArray(),
                    FileName = sfd.FileName
                };
                
                progressBar.Minimum = 0;
                progressBar.Value = 0;
                backgroundWorker.RunWorkerAsync(_exportModel);
            }

        }

        private void ImportFromCSV(object sender, EventArgs e)
        {
            ShowImportMenuView?.Invoke(this, e);
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var list = ((ExportModel)e.Argument).Data;
            var fileName = ((ExportModel)e.Argument).FileName;
            var index = 1;
            var process = list.Length;
            using var sw = new StreamWriter(new FileStream(fileName, FileMode.Create), Encoding.UTF8);
            var sb = new StringBuilder();
            sb.AppendLine("CustomerName, CustomerSurname, CustomerLastname, CustomerPhone, CarBrand, CarModel, BoxNumber, StartDate, EndDate, Description, Price");
                
            foreach (DataForProcessing p in list)
            {
                if (!backgroundWorker.CancellationPending)
                {
                    backgroundWorker.ReportProgress(index++ * 100 / process);
                    sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", p.CustomerName, p.CustomerSurname, p.CustomerLastname, p.CustomerPhone,
                        p.CarBrand, p.CarModel, p.BoxNumber, p.StartDate, p.EndDate, p.Description, p.Price));
                }
            }
            sw.Write(sb.ToString());
            
            MessageBox.Show("Data has been successfully exported",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            progressBar.Value = 0;
            labelStatus.Text = "Processing...0%";
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            labelStatus.Text = string.Format("Processing...{0}%", e.ProgressPercentage);
            progressBar.Update();
        }
    }
}