namespace Artsofte.Models;

public class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Age { get; set; }
    public string? Gender { get; set; }
    public int DepartamentId { get; set; }
    public int ProgrammingLanguageId { get; set; }
}
