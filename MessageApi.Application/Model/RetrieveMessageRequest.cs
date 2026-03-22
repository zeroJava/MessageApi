namespace MessageApi.Application;

public record RetrieveMessageRequest
{
   public required string UserAccessToken { get; set; }
   public required string Username { get; set; }
   public required string SenderEmailAddress { get; set; }
   public required string ReceiverEmailAddress { get; set; }
   public required long MessageIdThreshold { get; set; }
   public required int NumberOfMessages { get; set; }
}