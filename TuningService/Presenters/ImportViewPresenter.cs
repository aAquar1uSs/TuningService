using System.Data;
using TuningService.Repository;
using TuningService.Views;

namespace TuningService.Presenters;

public class ImportViewPresenter
{
    private readonly IImportMenuView _importMenuView;
    private readonly ICommonRepository _commonRepository;

    public ImportViewPresenter(IImportMenuView importMenuView, ICommonRepository commonRepository)
    {
        _importMenuView = importMenuView;
        _commonRepository = commonRepository;

        _importMenuView.GetDataFromCSVFile += GetDataFromCsvFile;
        _importMenuView.SaveDataFromCSVFile += SaveDataFromCSVFile;
    }

    private void GetDataFromCsvFile(string csvFile)
    {
        DataTable csvData = new DataTable();

        if (csvFile.EndsWith(".csv"))
        {
            using var csvReader = new Microsoft.VisualBasic.FileIO.TextFieldParser(csvFile);
            csvReader.SetDelimiters(",");
            csvReader.HasFieldsEnclosedInQuotes = false;
            //read column
            var colFields = csvReader.ReadFields();
            foreach (string column in colFields)
            {
                csvData.Columns.Add(new DataColumn(column));
            }

            while (!csvReader.EndOfData)
            {
                string[] fieldData = csvReader.ReadFields();
                for (int i = 0; i < fieldData.Length; i++)
                {
                    if (fieldData[i] == "")
                    {
                        fieldData[i] = null;
                    }
                }

                csvData.Rows.Add(fieldData);
            }
        }

        _importMenuView.SetAllDataToDataGridView(csvData);
    }

    private async void SaveDataFromCSVFile(DataTable dataTable)
    {
        await _commonRepository.Insert(dataTable);
    }
}