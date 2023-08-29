using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }
    [HttpPost]
    public async Task<ActionResult<AccountDto>> CreateAccountAsync([FromBody] CreateAccountModel model)
    {
        AccountDto account = await _service.CreateAccountAsync(model.Login, model.Password, model.EmployeeId);
        return Ok(account);
    }
}