using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthorizationController
{
    private readonly IAuthorizationService _service;

    public AuthorizationController(IAuthorizationService service)
    {
        _service = service;
    }
    
    [HttpPost("Authorization")]
    public async Task<EmployeeDto> Authorization([FromBody] AuthorizationModel model)
    {
        return await _service.Authorization(model.Login, model.Password);
    }
}