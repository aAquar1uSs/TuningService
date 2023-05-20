using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Npgsql;
using TuningService.Models.ViewModels;
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
        using var reader = new StreamReader(csvFile);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<DataForImport>();
        _importMenuView.SetAllDataToDataGridView(records.ToArray());
    }

    private async void SaveDataFromCSVFile(DataTable dataTable)
    {
        var dataFroImport = ExtractDataForImports(dataTable);
        
        await _commonRepository.Insert(dataFroImport);
    }

    private static List<DataForImport> ExtractDataForImports(DataTable dataTable)
    {
        var dataForImports = new List<DataForImport>();

        foreach (DataRow row in dataTable.Rows)
        {
            var dataForImport = new DataForImport
            {
                CustomerName = row["CustomerName"].ToString(),
                CustomerSurname = row["CustomerSurname"].ToString(),
                CustomerLastname = row["CustomerLastname"].ToString(),
                CustomerPhone = row["CustomerPhone"].ToString(),
                CarBrand = row["CarBrand"].ToString(),
                CarModel = row["CarModel"].ToString(),
                BoxNumber = Convert.ToInt32(row["BoxNumber"]),
                StartDate = Convert.ToDateTime(row["StartDate"]),
                EndDate = Convert.ToDateTime(row["EndDate"]),
                Description = row["Description"].ToString(),
                Price = Convert.ToDecimal(row["Price"])
            };

            dataForImports.Add(dataForImport);
        }

        return dataForImports;
    }
}