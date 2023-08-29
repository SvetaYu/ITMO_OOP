using Application.Exceptions;
using DataAccess;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace Application.Dto;

public class CreatePhoneHandler : Handler
{
    public override async Task<Message> HandleRequest(DatabaseContext context, CreateMessageDto message)
    {
        if (message.Content is PhoneContentDto phone)
        {
            if (message.Type != MessageTypeDto.Phone) throw MessageException.InvalidMessageType();
            PhoneMessageSource source = await context.MessageSources
                .OfType<PhoneMessageSource>()
                .SingleOrDefaultAsync(x => x.Number.Equals(phone.Phone));

            if (source is null)
                throw MessageSourceException.MessageSourceNotFound(phone.Phone);

            return new PhoneMessage
                (source,phone.Text, MessageState.Received);
        }

        if (Next is null)
        {
            throw new Exception();
        }

        return await Next.HandleRequest(context, message);
    }
}