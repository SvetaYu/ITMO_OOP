using System.Diagnostics;
using Application.Dto;
using DataAccess.Model;

namespace Application.Extensions;

public static class MessageAsDto
{
    public static MessageDto AsDto(this Message message)
    {
        MessageTypeDto type;
        MessageContentDto content;
        switch (message)
        {
            case EmailMessage emailMessage:
            {
                type = MessageTypeDto.Email;
                string email =
                    (emailMessage.MessageSource as EmailMessageSource)?.Email;
                string subject = emailMessage.Subject;
                string body = emailMessage.Text;
                content = new EmailContentDto(email, subject, body);
                break;
            }
            case PhoneMessage phoneMessage:
            {
                type = MessageTypeDto.Phone;
                string number =
                    (phoneMessage.MessageSource as PhoneMessageSource)?.Number;
                string body = phoneMessage.Text;
                content = new PhoneContentDto(number, body);
                break;
            }
            default:
            {
                throw new Exception();
            }
            
        }

        return new MessageDto(message.Id, type, content, message.MessageSource.Id, message.Time, message.State);
    }
}