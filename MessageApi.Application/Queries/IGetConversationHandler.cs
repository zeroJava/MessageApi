namespace MessageApi.Application;

public interface IGetConversationHandler
{
   Task<List<MessageInfo>> Handle(ConversationRequest conversationRequest);
}

public record ConversationRequest
{
   public required string SenderEmailAddress { get; set; }
   public required string ReceiverEmailAddress { get; set; }
   public required long MessageIdThreshold { get; set; }
   public required int NumberOfMessages { get; set; }
}