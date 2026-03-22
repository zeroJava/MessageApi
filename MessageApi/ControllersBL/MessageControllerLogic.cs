using MessageApi.Application;

namespace MessageApi.ControllersBL;

public static class MessageControllerLogic
{
   public static async Task<MessageRequestState> NewMessage(MessageRequest request, IMessageControllerCreatorBuilder builder)
   {
      MessageControllerCreatorOption option = builder.Build();
      IMessageCreatorUseCase messageService = option.NewMessageService;
      return await messageService.Create(request).ConfigureAwait(false);
   }

   public static async Task<List<MessageInfo>> GetMessagesSentToUser(RetrieveMessageRequest request, IMessageControllerRetrieverBuilder builder)
   {
      MessageControllerRetrieverOption option = builder.Build();
      IMessageRetrieverUseCase messageService = option.RetrieveMessageService;
      return await messageService.GetMessagesSentToUser(request).ConfigureAwait(false);
   }

   public static async Task<List<MessageInfo>> GetConversation(RetrieveMessageRequest request, IMessageControllerRetrieverBuilder builder)
   {
      MessageControllerRetrieverOption option = builder.Build();
      IMessageRetrieverUseCase messageService = option.RetrieveMessageService;
      return await messageService.GetConversation(request).ConfigureAwait(false);
   }
}