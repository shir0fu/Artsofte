﻿using Artsofte.Services;
using Microsoft.AspNetCore.Mvc;

namespace Artsofte.Controllers;

public class HomeController : Controller
{
    private readonly IEmployeeService _employeeService;

    public HomeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public IActionResult Index()
    {
        return View(_employeeService.GetEmployees());
    }

}