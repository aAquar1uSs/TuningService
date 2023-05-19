using System.Data;
using System.Windows.Forms;

namespace TuningService.Views.Impl;

public partial class ImportMenuView : Form, IImportMenuView
{
    private static ImportMenuView _importMenuView;

    public ImportMenuView()
    {
        InitializeComponent();
        buttonAccept.Click += (_, _) => SaveDataFromCSVFile((DataTable)dtgView.DataSource);
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

    public void SetAllDataToDataGridView(DataTable dt)
    {
        dtgView.DataSource = dt;
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

    private void buttonSelectFile_Click(object sender, System.EventArgs e)
    {
        var dialog = new OpenFileDialog();
        dialog.ShowDialog();
        
        if (dialog.FileName != "" && dialog.FileName.EndsWith(".csv"))
        {
            GetDataFromCSVFile?.Invoke(dialog.FileName);
        }
        else
        {
            MessageBox.Show("Selected File is Invalid, Please Select valid csv file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }


    private void buttonAccept_Click(object sender, System.EventArgs e)
    {
        SaveDataFromCSVFile?.Invoke((DataTable)dtgView.DataSource);
    }

    private void buttonClose_Click(object sender, System.EventArgs e)
    {
        Dispose();
        Close();
    }
}