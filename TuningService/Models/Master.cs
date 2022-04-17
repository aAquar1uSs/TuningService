namespace TuningService.Models;

public sealed class Master
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Phone { get; set; }

    public Master(int id, string name,
        string surname, string phone)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Phone = phone;
    }
}