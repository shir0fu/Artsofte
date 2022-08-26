using Artsofte.Services;
using Microsoft.AspNetCore.Mvc;

namespace Artsofte.Controllers;

public class AddingController : Controller
{
    private readonly IEmployeeService _employeeService;

    public AddingController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public IActionResult Index()
    {
        return View(_employeeService.GetEmployees());
    }
}
