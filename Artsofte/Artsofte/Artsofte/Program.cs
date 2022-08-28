using Microsoft.EntityFrameworkCore;
using Artsofte.Models;
using Artsofte.Services;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddMvc();

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
pattern: "{controller=Home}/{action=GetEmployeesAsync}");

app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=AddEmployee}");

app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=DeleteEmployeeAsync}/{id?}");


app.Run();
