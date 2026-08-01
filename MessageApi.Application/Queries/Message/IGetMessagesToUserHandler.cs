namespace MessageApi.Application;

public interface IGetMessagesToUserHandler
{
   Task<IEnumerable<MessageInfo>> Handle(MessagesToUserRequest request);
}

public record MessagesToUserRequest
{
   public required string Username { get; set; }
   public required string ReceiverEmailAddress { get; set; }
}