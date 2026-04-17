namespace MessageApi.Application;

public class RetrieveMessageService : IMessageRetrieverUseCase
{
   readonly IMessageRetriever messageRetriever;

   public RetrieveMessageService(IMessageRetriever messageRetriever)
   {
      this.messageRetriever = messageRetriever;
   }

   public async Task<List<MessageInfo>> GetConversation(RetrieveMessageRequest messageRequest)
   {
      return messageRetriever.GetConversation(messageRequest);
   }

   public async Task<List<MessageInfo>> GetMessagesSentToUser(RetrieveMessageRequest messageRequest)
   {
      return messageRetriever.GetMessagesSentToUser(messageRequest);
   }
}