using Application.Exceptions;
using DataAccess;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace Application.Dto;

public class CreateEmailHandler : Handler
{
    public override async Task<Message> HandleRequest(DatabaseContext context, CreateMessageDto message)
    {
        if (message.Content is EmailContentDto email)
        {
            if (message.Type != MessageTypeDto.Email) throw MessageException.InvalidMessageType();
            EmailMessageSource source = await context.MessageSources
                .OfType<EmailMessageSource>()
                .SingleOrDefaultAsync(x => x.Email.Equals(email.Email));

            if (source is null)
                throw MessageSourceException.MessageSourceNotFound(email.Email);

            return new EmailMessage
                (source, email.Subject, email.Body, MessageState.Received);
        }

        if (Next is null)
        {
            throw new Exception();
        }

        return await Next.HandleRequest(context, message);
    }
}