﻿using System;
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
        public DeleteMasterView()
        {
            InitializeComponent();
        }

        public Master MasterInfo { get => _master; set => _master = value; }

        public event EventHandler UpdateListOfMastersEvent;

        public event EventHandler DeleteMasterEvent;

        public static DeleteMasterView GetInstance()
        {
            if (_deleteMasterViewInstance is null || _deleteMasterViewInstance.IsDisposed)
            {
                _deleteMasterViewInstance = new DeleteMasterView();
                _deleteMasterViewInstance.FormBorderStyle = FormBorderStyle.FixedSingle;
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
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var masterInfo = comboBoxMasters.Text.Split(' ');
            var name = masterInfo[0];
            var surname = masterInfo[1];

            _master = MasterFactory.GetMasterInstance(name, surname);

            DeleteMasterEvent?.Invoke(this, EventArgs.Empty);

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
