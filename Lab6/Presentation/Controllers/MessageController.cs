using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _service;

    public MessageController(IMessageService service)
    {
        _service = service;
    }
    
    [HttpPost("receive")]
    public async Task<ActionResult<MessageDto>> ReceiveMessageAsync([FromBody]CreateMessageDto message)
    {
       MessageDto newMessage = await _service.ReceiveMessageAsync(message);
       return Ok(newMessage);
    }

    [HttpPost("respond")]
    public async Task RespondMessageAsync(Guid id, CreateMessageDto message, [FromQuery] Guid employeeId)
    {
        await _service.RespondMessageAsync(id, message, employeeId);
    }
    
    [HttpPost("read")]
    public async Task<ActionResult<MessageDto>> ReadMessageAsync([FromBody]Guid id, [FromQuery] Guid employeeId)
    {
        MessageDto message =  await _service.ReadMessageAsync(id, employeeId);
        return Ok(message);
    }
    
}