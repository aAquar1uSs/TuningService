using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TuningService.Factories;
using TuningService.Models.ViewModels;

namespace TuningService.Views.Impl
{
    public partial class DeleteMasterView : Form, IDeleteMasterView
    {
        private static DeleteMasterView _deleteMasterViewInstance;
        private DataTable _dataTable;

        private DeleteMasterView()
        {
            InitializeComponent();
        }

        public event EventHandler UpdateListOfMastersEvent;

        public event DeleteMasterDelegate DeleteAndReplaceMasterEvent;

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

        protected override void WndProc(ref Message m)
        {
            const int WM_CLOSE = 0x0010;

            if (m.Msg == WM_CLOSE)
            {
                Dispose();
                return;
            }

            base.WndProc(ref m);
        }
        
        public void SetDataAboutMasters(IEnumerable<MasterViewModel> masterViewModels)
        {
            if (!masterViewModels.Any())
                return;
            
            comboBoxMasters.Items.AddRange(masterViewModels.Select(x => x.MasterInfo).ToArray());
            
            comboBoxMasters.DisplayMember = "concat";
            comboBoxMasters.ValueMember = "concat";
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("You sure?",
               "Information",
               MessageBoxButtons.OKCancel,
               MessageBoxIcon.Information);

            if (result == DialogResult.Cancel)
                return;

            var oldMasterInfo = comboBoxMasters.Text.Split(' ');
            var name = oldMasterInfo[0];
            var surname = oldMasterInfo[1];

            var oldMaster = MasterFactory.GetMasterInstance(name, surname);

            var newMasterInfo = comboBoxMasterRep.Text.Split(' ');
            name = newMasterInfo[0];
            surname = newMasterInfo[1];

            var newMaster = MasterFactory.GetMasterInstance(name, surname);

            await DeleteAndReplaceMasterEvent?.Invoke(oldMaster, newMaster);

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

        private void comboBoxMasters_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var selectedMaster = comboBoxMasters.Text;

            comboBoxMasterRep.Items.Clear();

            for (var i = 0; i < comboBoxMasters.Items.Count; i++)
            {
                var item = comboBoxMasters.Items[i].ToString();
                if (!item.Equals(selectedMaster, StringComparison.InvariantCulture))
                    comboBoxMasterRep.Items.Add(item);
            }
            
            buttonDelete.Enabled = true;
        }
    }
}
