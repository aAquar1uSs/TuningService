using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using TuningService.Models.ViewModels;

namespace TuningService.Views.Impl;

public partial class ImportMenuView : Form, IImportMenuView
{
    private static ImportMenuView _importMenuView;

    public ImportMenuView()
    {
        InitializeComponent();

        buttonAccept.Enabled = false;
    }

    public event GetDataFromCSV GetDataFromCSVFile;
    public event SaveDataFromCSV SaveDataFromCSVFile;

    public static ImportMenuView GetInstance()
    {
        if (_importMenuView is null || _importMenuView.IsDisposed)
        {
            _importMenuView = new ImportMenuView();
            _importMenuView.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        else
        {
            if (_importMenuView.WindowState == FormWindowState.Minimized)
                _importMenuView.WindowState = FormWindowState.Normal;
        }

        return _importMenuView;
    }

    public void SetAllDataToDataGridView(IReadOnlyCollection<DataForProcessing> dataForImports)
    {
        var dataTable = new DataTable();
        var properties = typeof(DataForProcessing).GetProperties();

        foreach (var property in properties)
        {
            dataTable.Columns.Add(property.Name, property.PropertyType);
        }
        
        foreach (var data in dataForImports)
        {
            dataTable.LoadDataRow(new object[]
            {
                data.CustomerName,
                data.CustomerSurname,
                data.CustomerLastname,
                data.CustomerPhone,
                data.CarBrand,
                data.CarModel,
                data.BoxNumber,
                data.StartDate,
                data.EndDate,
                data.Description,
                data.Price,
            }, true);
        }

        dtgView.DataSource = dataTable;
    }

    private void ImportMenuView_Load(object sender, System.EventArgs e)
    {
        
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

    private void buttonSelectFile_Click(object sender, EventArgs e)
    {
        var dialog = new OpenFileDialog();
        dialog.ShowDialog();
        
        try
        {
            if (dialog.FileName != "" && dialog.FileName.EndsWith(".csv"))
            {
                GetDataFromCSVFile?.Invoke(dialog.FileName);
                buttonAccept.Enabled = true;
            }
            else
            {
                MessageBox.Show("Selected File is Invalid, Please Select valid csv file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show($"Failed to import data due to error.Wrong data format. Reason: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


    private void buttonAccept_Click(object sender, EventArgs e)
    {
        SaveDataFromCSVFile?.Invoke((DataTable)dtgView.DataSource);

        buttonAccept.Enabled = false;
        
        MessageBox.Show("Data has been successfully addded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
        Dispose();
        Close();
    }

    private void buttonClose_Click(object sender, System.EventArgs e)
    {
        Dispose();
        Close();
    }
}