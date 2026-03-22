namespace MessageApi.Application;

public enum MessageReceivedState
{
   Successful,
   FailedToProcess,
}

public record MessageRequestState
{
   public long MessageId { get; set; }
   public required string Message { get; set; }
   public required MessageReceivedState MessageRecievedState { get; set; }
}