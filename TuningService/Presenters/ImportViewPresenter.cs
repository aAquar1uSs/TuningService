using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
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
        var dataForImports = new List<DataForImport>();

        if (csvFile.EndsWith(".csv"))
        {
            using var reader = new StreamReader(csvFile);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine().TrimEnd('"');
                var fields = line?.Split(',');
                var dataForImport = new DataForImport
                {
                    CustomerName = fields[0],
                    CustomerSurname = fields[1],
                    CustomerLastname = fields[2],
                    CustomerPhone = fields[3],
                    CarBrand = fields[4],
                    CarModel = fields[5],
                    BoxNumber = Convert.ToInt32(fields[6]),
                    StartDate = Convert.ToDateTime(fields[7]),
                    EndDate = Convert.ToDateTime(fields[8]),
                    Description = fields[9],
                    Price = decimal.Parse(fields[10], NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture)
                };

                dataForImports.Add(dataForImport);
            }
        }

        _importMenuView.SetAllDataToDataGridView(dataForImports);
    }

    private async void SaveDataFromCSVFile(DataTable dataTable)
    {
        var dataFroImport = ExtractDataForImports(dataTable);
        
        await _commonRepository.Insert(dataFroImport);
    }

    private static List<DataForImport> ExtractDataForImports(DataTable dataTable)
    {
        List<DataForImport> dataForImports = new List<DataForImport>();

        foreach (DataRow row in dataTable.Rows)
        {
            DataForImport dataForImport = new DataForImport();
            dataForImport.CustomerName = row["CustomerName"].ToString();
            dataForImport.CustomerSurname = row["CustomerSurname"].ToString();
            dataForImport.CustomerLastname = row["CustomerLastname"].ToString();
            dataForImport.CustomerPhone = row["CustomerPhone"].ToString();
            dataForImport.CarBrand = row["CarBrand"].ToString();
            dataForImport.CarModel = row["CarModel"].ToString();
            dataForImport.BoxNumber = Convert.ToInt32(row["BoxNumber"]);
            dataForImport.StartDate = Convert.ToDateTime(row["StartDate"]);
            dataForImport.EndDate = Convert.ToDateTime(row["EndDate"]);
            dataForImport.Description = row["Description"].ToString();
            dataForImport.Price = Convert.ToDecimal(row["Price"]);

            dataForImports.Add(dataForImport);
        }

        return dataForImports;
    }
}