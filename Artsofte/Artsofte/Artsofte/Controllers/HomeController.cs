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


    public async Task<IActionResult> Index()
    {
        return View(await _employeeService.GetEmployeesAsync());
    }


    [HttpGet("/edit/{id?}")]
    public async Task<IActionResult> UpdateEmployee([FromRoute]int id)
    {
        return View(await _employeeService.GetEmployeeByIdAsync(id));
    }


    [HttpPost("/edit/{id}")]
    public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDTO updateEmployeeDTO)
    {
        bool result = await _employeeService.UpdateEmployeeAsync(updateEmployeeDTO);
        if (result)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return RedirectToAction("Error");
        }
    }


    [HttpGet("/add")]
    public IActionResult AddEmployee()
    {
        return View("AddEmployee");
    }


    [HttpPost("/add")]
    public async Task<IActionResult> AddEmployee(CreateEmployeeDTO createEmployeeDTO)
    {
        bool result = await _employeeService.AddEmployeeAsync(createEmployeeDTO);
        if (result)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return RedirectToAction("Error");
        }
    }


    [HttpPost("/Home/DeleteEmployee/{id}")]
    public async Task<IActionResult> DeleteEmployee([FromRoute]int id)
    {
        bool result = await _employeeService.DeleteEmployeeAsync(id);
        if (result)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return BadRequest();
        }
    }


    public IActionResult Error()
    {
        return View("Error");
    }
}