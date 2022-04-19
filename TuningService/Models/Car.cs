using System.ComponentModel.DataAnnotations;

namespace TuningService.Models;

public class Car
{
    public int Id { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Car name must be between 2 and 30 characters")]
    public string Name { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Car model must be between 2 and 30 characters")]
    public string Model { get; set; }

    public Customer Owner { get; set; }

    public Car(string name,
        string model)
    {
        Name = name;
        Model = model;
    }
}