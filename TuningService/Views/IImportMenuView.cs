using System.Collections.Generic;
using System.Data;
using TuningService.Models.ViewModels;

namespace TuningService.Views;

public delegate void GetDataFromCSV(string csv_file_path);

public delegate void SaveDataFromCSV(DataTable dataTable);

public interface IImportMenuView
{
    event GetDataFromCSV GetDataFromCSVFile;

    event SaveDataFromCSV SaveDataFromCSVFile;
    
    void SetAllDataToDataGridView(IReadOnlyCollection<DataForImport> dataForImports);
}