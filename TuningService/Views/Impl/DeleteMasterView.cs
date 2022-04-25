using System;
using System.Data;
using System.Windows.Forms;
using TuningService.Factories;
using TuningService.Models;

namespace TuningService.Views.Impl
{
    public partial class DeleteMasterView : Form, IDeleteMasterView
    {

        private static DeleteMasterView _deleteMasterViewInstance;

        private Master _master;

        private DeleteMasterView()
        {
            InitializeComponent();
        }

        public event EventHandler UpdateListOfMastersEvent;

        public event DeleteMasterDelegate DeleteMasterEvent;

        public static DeleteMasterView GetInstance()
        {
            if (_deleteMasterViewInstance is null || _deleteMasterViewInstance.IsDisposed)
            {
                _deleteMasterViewInstance = new DeleteMasterView
                {
                    FormBorderStyle = FormBorderStyle.FixedSingle
                };
            }
            else
            {
                if (_deleteMasterViewInstance.WindowState == FormWindowState.Minimized)
                    _deleteMasterViewInstance.WindowState = FormWindowState.Normal;
            }

            return _deleteMasterViewInstance;
        }

        public void SetDataAboutMasters(DataTable dt)
        {
            comboBoxMasters.DataSource = dt;
            comboBoxMasters.DisplayMember = "concat";
            comboBoxMasters.ValueMember = "concat";
        }
        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("If you remove this master, remove the orders that it has executed",
               "Information",
               MessageBoxButtons.OKCancel,
               MessageBoxIcon.Information);

            if (result == DialogResult.Cancel)
                return;


            var masterInfo = comboBoxMasters.Text.Split(' ');
            var name = masterInfo[0];
            var surname = masterInfo[1];

            _master = MasterFactory.GetMasterInstance(name, surname);

            await DeleteMasterEvent?.Invoke(_master);

            MessageBox.Show("Master has been successfully deleted!",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            Close();
        }

        private void DeleteMasterView_Load(object sender, EventArgs e)
        {
            UpdateListOfMastersEvent?.Invoke(this, EventArgs.Empty);
            buttonClose.Click += (_, _) => Close();
        }
    }
}
