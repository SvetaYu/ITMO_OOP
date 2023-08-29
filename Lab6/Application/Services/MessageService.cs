using Application.Dto;
using Application.Exceptions;
using Application.Extensions;
using Application.Services.Interfaces;
using DataAccess;
using DataAccess.Model;

namespace Application.Services;

public class MessageService : IMessageService
{
    private readonly DatabaseContext _context;

    public MessageService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<MessageDto> ReadMessageAsync(Guid messageId, Guid employeeId)
    {
        Message message = await _context.Messages.GetEntityAsync(messageId, default);
        message.State = MessageState.Read;
        return message.AsDto();
    }

    public async Task RespondMessageAsync(Guid toId, CreateMessageDto responseDto, Guid employeeId)
    {
        Message toMessage = await _context.Messages.GetEntityAsync(toId, default);
        if (responseDto.Type != toMessage.AsDto().Type) throw MessageException.InvalidMessageType();
        Message response = await CreateMessageAsync(responseDto);
        Employee employee = await _context.Employees.GetEntityAsync(employeeId, default);

        var messageResponse = new MessageResponse(response, toMessage, employee);
        toMessage.State = MessageState.Processed;

        _context.MessagesResponse.Add(messageResponse);
        await _context.SaveChangesAsync();
    }

    public async Task<MessageDto> ReceiveMessageAsync(CreateMessageDto messageDto)
    {
        Message message = await CreateMessageAsync(messageDto);
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return message.AsDto();
    }

    private async Task<Message> CreateMessageAsync(CreateMessageDto messageDto)
    {
        var chain = new CreateMessageChain(new CreateEmailHandler(), new CreatePhoneHandler());
        Message message = await chain.Execute(_context, messageDto);
        return message;
    }
}