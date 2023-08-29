using Application.Dto;
using DataAccess.Model;

namespace Application.Extensions;

public static class MessageSourceAsDto
{
    public static MessageSourceDto AsDto(this MessageSource source)
    {
        MessageTypeDto type;
        MessageSourceNameDto name;
        switch (source)
        {
            case EmailMessageSource emailMessageSource:
            {
                type = MessageTypeDto.Email;
                name = new EmailNameDto(emailMessageSource.Email);
                break;
            }
            case PhoneMessageSource phoneMessageSource:
            {
                type = MessageTypeDto.Phone;
                name = new PhoneNameDto(phoneMessageSource.Number);
                break;
            }
            default:
            {
                throw new Exception();
            }
            
        }
            
       return new MessageSourceDto(source.Id, type, name);
    }
}