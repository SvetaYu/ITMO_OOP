using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageSourceController
{
    private readonly IMessageSourceService _service;

    public MessageSourceController(IMessageSourceService service)
    {
        _service = service;
    }
    [HttpPost]
    public async Task<ActionResult<MessageSourceDto>> CreateMessageSourceAsync([FromBody] CreateMessageSourceModel model)
    {
       MessageSourceDto messageSource =  await _service.CreateMessageSource(model.Type, model.Name);
       return new OkObjectResult(messageSource);
    }
}