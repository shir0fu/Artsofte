using Microsoft.EntityFrameworkCore;
using Artsofte.DTO;

namespace Artsofte.Models;

public class ApplicationContext : DbContext
{
    public DbSet<Employee> Employee{ get; set; } = null!;
    public DbSet<Departament> Departament { get; set; } = null!;
    public DbSet<ProgrammingLanguage> ProgrammingLanguage { get; set; } = null!;


    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }


}

