﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;
using TuningService.Factories;
using TuningService.Models;

namespace TuningService.Views.Impl
{
    public partial class NewMasterView : Form, INewMasterView
    {
        private static NewMasterView _newMasterViewInstance;

        private Master _master;

        private NewMasterView()
        {
            InitializeComponent();
        }

        public Master Master { get => _master; set => _master = value; }

        public event EventHandler AddNewMaster;

        public static NewMasterView GetInstance()
        {
            if (_newMasterViewInstance is null || _newMasterViewInstance.IsDisposed)
            {
                _newMasterViewInstance = new NewMasterView();
                _newMasterViewInstance.FormBorderStyle = FormBorderStyle.FixedSingle;
            }
            else
            {
                if (_newMasterViewInstance.WindowState == FormWindowState.Minimized)
                    _newMasterViewInstance.WindowState = FormWindowState.Normal;
            }

            return _newMasterViewInstance;

        }

        private void NewMasterView_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            _newMasterViewInstance.Close();
        }

        private void buttonAddMaster_Click(object sender, EventArgs e)
        {
            var name = textBoxName.Text;
            var surname = textBoxSurname.Text;
            var phone = textBoxPhone.Text;

            try
            {
                _master = MasterFactory.GetMasterInstance(name, surname, phone);
            }
            catch (ValidationException)
            {
                MessageBox.Show("Incorrect data entered!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }
            AddNewMaster?.Invoke(this, EventArgs.Empty);

            MessageBox.Show("Master has been successfully added!",
                    "Inforamtion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            _newMasterViewInstance.Close();

        }
    }
}
