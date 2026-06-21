namespace MessageApi.Application;

public interface IGetMessagesToUserHandler
{
   Task<List<MessageInfo>> Handle(MessagesToUserRequest request);
}

public record MessagesToUserRequest
{
   public required string SenderEmailAddress { get; set; }
}