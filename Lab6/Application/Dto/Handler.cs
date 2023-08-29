using DataAccess;
using DataAccess.Model;

namespace Application.Dto;

public abstract class Handler
{
    public Handler Next { get; set; }
    public abstract Task<Message> HandleRequest(DatabaseContext context, CreateMessageDto message);
}