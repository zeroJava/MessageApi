namespace MessageApi.Application;

public interface IMessageServiceUseCase
{
   Task<MessageRequestState> Create(MessageRequest request);
   Task<List<MessageInfo>> GetMessagesSentToUser(RetrieveMessageRequest messageRequest);
   Task<List<MessageInfo>> GetConversation(RetrieveMessageRequest messageRequest);
}