using System.Data;
using Npgsql;
using TuningService.Repository;
using TuningService.Repository.Impl;
using TuningService.Utilites.Settings;
using TuningService.Views;

namespace TuningService.Presenters;

public class ImportViewPresenter
{
    private readonly IImportMenuView _importMenuView;
    private readonly ICommonRepository _commonRepository;

    public ImportViewPresenter(IImportMenuView importMenuView)
    {
        _importMenuView = importMenuView;
        _commonRepository = new CommonRepository(new NpgsqlConnection(AppConnection.ConnectionString));

        _importMenuView.GetDataFromCSVFile += GetDataFromCsvFile;
        _importMenuView.SaveDataFromCSVFile += SaveDataFromCSVFile;
    }

    private void GetDataFromCsvFile(string csvFile)
    {
        var csvData = new DataTable();

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