namespace MessageApi.Domain;

public class Message
{
   public required long Id { get; set; }
   public required string MessageText { get; set; }
   public long? SenderId { get; set; }
   public string? SenderEmailAddress { get; set; }
   public DateTime? MessageCreated { get; set; }
   public User? User { get; set; }
   public HashSet<MessageDispatch> MessageDispatches { get; set; }

   public Message()
   {
      MessageDispatches = new();
   }
}