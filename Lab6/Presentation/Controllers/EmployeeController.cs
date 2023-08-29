using Application.Dto;
using Application.Services.Interfaces;
using DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeeController( IEmployeeService service)
    {
        _service = service;

    }
    
    [HttpPost("CreateEmployee")]
    public async Task<ActionResult<Employee>> CreateEmployeeAsync([FromBody] CreateEmployeeModel model)
    {
        EmployeeDto employee = await _service.CreateEmployeeAsync(model.Name, Guid.NewGuid());
        return Ok(employee);
    }
    
    [HttpPost("CreateManager")]
    public async Task<ActionResult<Employee>> CreateManagerAsync([FromBody] CreateManagerModel model)
    {
        ManagerDto manager = await _service.CreateManagerAsync(model.Name, Guid.NewGuid(), model.SubordinatesId);
        return new OkObjectResult(manager);
    }
}