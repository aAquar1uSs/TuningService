using System.ComponentModel.DataAnnotations;

namespace TuningService.Models;

public class Car
{
    public int CarId { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Car name must be between 2 and 30 characters")]
    public string Brand { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Car model must be between 2 and 30 characters")]
    public string Model { get; set; }

    public Customer Owner { get; set; }
}