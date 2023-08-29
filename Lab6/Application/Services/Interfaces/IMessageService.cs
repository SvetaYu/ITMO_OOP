using Application.Dto;

namespace Application.Services.Interfaces;

public interface IMessageService
{
    Task RespondMessageAsync(Guid toId, CreateMessageDto response, Guid employeeId);
    Task<MessageDto> ReadMessageAsync(Guid messageId, Guid employeeId);
    Task<MessageDto> ReceiveMessageAsync(CreateMessageDto messageDto);
}