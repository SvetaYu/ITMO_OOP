using Application.Dto;
using Application.Exceptions;
using Application.Extensions;
using Application.Services.Interfaces;
using DataAccess;
using DataAccess.Model;

namespace Application.Services;

public class MessageSourceService : IMessageSourceService
{
    private readonly DatabaseContext _context;

    public MessageSourceService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<MessageSourceDto> CreateMessageSource(MessageTypeDto type, MessageSourceNameDto nameDto)
    {
        MessageSource source;
        switch (type)
        {
            case MessageTypeDto.Email:
            {
                if (nameDto is EmailNameDto email)
                {
                    source = new EmailMessageSource(email.Email);
                }
                else
                {
                    throw MessageException.InvalidMessageType();
                }

                break;
            }
            case MessageTypeDto.Phone:
            {
                if (nameDto is PhoneNameDto phone)
                {
                    source = new PhoneMessageSource(phone.Number);
                }
                else
                {
                    throw MessageException.InvalidMessageType();
                }
                break;
            }
            default:
                throw new Exception();
        }
        
        _context.MessageSources.Add(source);

        await _context.SaveChangesAsync();
        return source.AsDto();
    }
}