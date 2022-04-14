using Npgsql;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using TuningService.Tools;

namespace TuningService
{
    public partial class Form1 : Form
    {

        private readonly DbConnection _dbConnection;

        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Normal;
            _dbConnection = DbConnection.GetInstance();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedState = comboBox1.SelectedItem.ToString();

            switch (selectedState)
            {
                case "Customers":
                    ShowTable("customer");
                    break;

                case "Cars":
                    ShowTable("car");
                    break;

                case "Masters":
                    ShowTable("master");
                    break;

                case "Tuning Boxes":
                    ShowTable("tuning_box");
                    break;

                case "Orders":
                    ShowTable("tuning_order");
                    break;

            }
        }

        private void ShowTable(string nameTable)
        {
            _dbConnection.Connection.Open();
            var command = new NpgsqlCommand();
            command.Connection = _dbConnection.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = $"SELECT * FROM {nameTable}";

            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
            }
            command.Dispose();
            _dbConnection.Connection.Close();
        }
    }
}