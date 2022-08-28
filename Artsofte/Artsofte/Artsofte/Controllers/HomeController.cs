using Artsofte.Services;
using Artsofte.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Artsofte.Controllers;

public class HomeController : Controller
{
    private readonly IEmployeeService _employeeService;

    public HomeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<IActionResult> GetEmployeesAsync()
    {
        return View(await _employeeService.GetEmployeesAsync());
    }

    [HttpGet("/add")]
    public IActionResult AddEmployee()
    {
        return View("AddEmployee");
    }

    [HttpPost("/add")]
    public async Task<IActionResult> AddEmployeeAsync(CreateEmployeeDTO createEmployeeDTO)
    {
        bool result = await _employeeService.AddEmployeeAsync(createEmployeeDTO);
        if (result)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost("/delete/{id?}")]
    public async Task<IActionResult> DeleteEmployeeAsync([FromQuery]int id)
    {

    }
}