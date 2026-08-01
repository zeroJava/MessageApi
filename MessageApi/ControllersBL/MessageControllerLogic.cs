using MessageApi.Application;

namespace MessageApi.ControllersBL;

public static class MessageControllerLogic
{
   public static async Task<MessageRequestState> NewMessage(MessageRequest request, IMessageControllerBuilder builder)
   {
      MessageControllerOption option = builder.Build();
      IMessageServiceUseCase messageService = option.MessageService;
      return await messageService.Create(request).ConfigureAwait(false);
   }

   public static async Task<IEnumerable<MessageInfo>> GetMessagesSentToUser(RetrieveMessageRequest request, IMessageControllerBuilder builder)
   {
      MessageControllerOption option = builder.Build();
      IMessageServiceUseCase messageService = option.MessageService;
      return await messageService.GetMessagesSentToUser(request).ConfigureAwait(false);
   }

   public static async Task<IEnumerable<MessageInfo>> GetConversation(RetrieveMessageRequest request, IMessageControllerBuilder builder)
   {
      MessageControllerOption option = builder.Build();
      IMessageServiceUseCase messageService = option.MessageService;
      return await messageService.GetConversation(request).ConfigureAwait(false);
   }
}