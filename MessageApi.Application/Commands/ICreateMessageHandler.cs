namespace MessageApi.Application;

public interface ICreateMessageHandler
{
   Task<MessageRequestState> Handle(CreateMessageRequest request);
}

public record CreateMessageRequest
{
   public required string UserName { get; set; }
   public required string Message { get; set; }
   public required List<string> EmailAccounts { get; set; }
   public required DateTime MessageCreated { get; set; }
}