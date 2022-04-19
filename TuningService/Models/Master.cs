
using System.ComponentModel.DataAnnotations;

namespace TuningService.Models;

public sealed class Master
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }

    [Required]
    [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$")]
    public string Phone { get; set; }

    public Master( string name,
        string surname, string phone)
    {
        Name = name;
        Surname = surname;
        Phone = phone;
    }
}