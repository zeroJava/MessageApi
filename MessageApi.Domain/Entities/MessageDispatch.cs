namespace MessageApi.Domain;

public class MessageDispatch
{
   public required long Id { get; set; }
   public required string EmailAddress { get; set; }
   public long? MessageId { get; set; }
   public bool? MessageReceived { get; set; }
   public DateTime? MessageReceivedTime { get; set; }
   public Message? Message { get; set; }
}