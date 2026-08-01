namespace MessageApi.Application;

public interface IMessageServiceUseCase
{
   Task<MessageRequestState> Create(MessageRequest request);
   Task<IEnumerable<MessageInfo>> GetMessagesSentToUser(RetrieveMessageRequest messageRequest);
   Task<IEnumerable<MessageInfo>> GetConversation(RetrieveMessageRequest messageRequest);
}