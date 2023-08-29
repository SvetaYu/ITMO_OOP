using DataAccess;
using DataAccess.Model;

namespace Application.Dto;

public class CreateMessageChain
{
    public CreateMessageChain(params Handler[] handlers)
    {
        Root = handlers.First();
        for (int i = 0; i < handlers.Length - 1; i++)
        {
            handlers[i].Next = handlers[i + 1];
        }
    }
    public Handler Root { get; set; }
    public Task<Message> Execute(DatabaseContext context, CreateMessageDto message) => Root.HandleRequest(context, message);
}