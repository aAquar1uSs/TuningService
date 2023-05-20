using System;

namespace TuningService.Models.ViewModels;

public class DataForImport
{
    public string CustomerName { get; set; }
    
    public string CustomerSurname { get; set; }
    
    public string CustomerLastname { get; set; }
    
    public string CustomerPhone { get; set; }
    
    public string CarBrand { get; set; }
    
    public string CarModel { get; set; }
    
    public int BoxNumber { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
}