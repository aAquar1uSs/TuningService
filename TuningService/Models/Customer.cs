using System.ComponentModel.DataAnnotations;

namespace TuningService.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Customer name must be between 3 and 30 characters")]
    public string Name { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Customer lastname must be between 3 and 30 characters")]
    public string Lastname { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Customer surname must be between 3 and 30 characters")]
    public string Surname { get; set; }

    [Required]
    [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{9}$")]
    public string Phone { get; set; }
}