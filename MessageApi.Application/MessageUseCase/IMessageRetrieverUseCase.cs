namespace MessageApi.Application;

public interface IMessageRetrieverUseCase
{
   Task<List<MessageInfo>> GetMessagesSentToUser(RetrieveMessageRequest messageRequest);
   Task<List<MessageInfo>> GetConversation(RetrieveMessageRequest messageRequest);
}