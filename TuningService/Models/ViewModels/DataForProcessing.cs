using System;
using CsvHelper.Configuration.Attributes;

namespace TuningService.Models.ViewModels;

public class DataForProcessing
{
    [Name("CustomerName")]
    public string CustomerName { get; set; }
    
    [Name("CustomerSurname")]
    public string CustomerSurname { get; set; }
    
    [Name("CustomerLastname")]
    public string CustomerLastname { get; set; }
    
    [Name("CustomerPhone")]
    public string CustomerPhone { get; set; }
    
    [Name("CarBrand")]
    public string CarBrand { get; set; }
    
    [Name("CarModel")]
    public string CarModel { get; set; }
    
    [Name("BoxNumber")]
    public int BoxNumber { get; set; }
    
    [Name("StartDate")]
    [Format("dd/MM/yyyy")]
    public DateTime StartDate { get; set; }
    
    [Name("EndDate")]
    [Format("dd/MM/yyyy")]
    public DateTime EndDate { get; set; }
    
    [Name("Description")]
    public string Description { get; set; }
    
    [Name("Price")]
    public decimal Price { get; set; }
}