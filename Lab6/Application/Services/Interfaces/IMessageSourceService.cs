using Application.Dto;

namespace Application.Services.Interfaces;

public interface IMessageSourceService
{
    Task<MessageSourceDto> CreateMessageSource(MessageTypeDto type, MessageSourceNameDto nameDto);
}