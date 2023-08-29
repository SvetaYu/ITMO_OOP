namespace DataAccess.Model;

public abstract class Message
{
   public Message(MessageSource source, MessageState state)
   {
      MessageSource = source;
      Id = Guid.NewGuid();
      State = state;
      Time = DateTime.Now;
   }

   protected Message() { }

   public Guid Id { get; set; }
   public virtual MessageSource MessageSource { get; set; }
   public MessageState State { get; set; }
   public DateTime Time { get; set; }
}