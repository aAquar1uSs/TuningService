using System.ComponentModel.DataAnnotations;

namespace TuningService.Models;

public sealed class TuningBox
{
    [Key]
    public int BoxId { get; set; }

    public int BoxNumber { get; set; }

    public Car Car { get; set; }

    public Master Master { get; set; }
}