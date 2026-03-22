namespace MessageApi.Application;

public record MessageInfo
{
   public string? SenderName { get; set; }
   public string? ReceiverName { get; set; }
   public DateTime? MessageSentDate { get; set; }
   public string? MessageContent { get; set; }
   public bool? MessageReceived { get; set; }
   public DateTime? MessageReceivedDate { get; set; }
   public bool SenderCurrentUser { get; set; }
   public long DispatchId { get; set; }
   public long MessageId { get; set; }
}