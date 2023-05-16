
using System.ComponentModel.DataAnnotations;

namespace TuningService.Models;

public sealed class Master
{
    public int MasterId { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Master name must be between 2 and 30 characters")]
    public string Name { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Master surname must be between 2 and 30 characters")]
    public string Surname { get; set; }

    [Required]
    [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{9}$")]
    public string Phone { get; set; }
}