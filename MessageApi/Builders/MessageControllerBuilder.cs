using MessageApi.Application;

namespace MessageApi;

public interface IMessageControllerBuilder
{
   IMessageControllerBuilder AddMessageService(IMessageServiceUseCase messageService);
   IMessageControllerBuilder AddCreateMessageHandler(ICreateMessageHandler createMessageHandler);
   IMessageControllerBuilder AddGetConversationHandler(IGetConversationHandler conversationHandler);
   IMessageControllerBuilder AddGetMessagesToUserHandler(IGetMessagesToUserHandler messagesToUserHandler);
   MessageControllerOption Build();
}

public sealed record MessageControllerOption
{
   public required IMessageServiceUseCase MessageService { get; set; }
}

public class MessageControllerBuilder : IMessageControllerBuilder
{
   IMessageServiceUseCase? messageService;
   ICreateMessageHandler? createMessageHandler;
   IGetConversationHandler? conversationHandler;
   IGetMessagesToUserHandler? messagesToUserHandler;

   public IMessageControllerBuilder AddCreateMessageHandler(ICreateMessageHandler createMessageHandler)
   {
      this.createMessageHandler = createMessageHandler;
      return this;
   }

   public IMessageControllerBuilder AddGetConversationHandler(IGetConversationHandler conversationHandler)
   {
      this.conversationHandler = conversationHandler;
      return this;
   }

   public IMessageControllerBuilder AddGetMessagesToUserHandler(IGetMessagesToUserHandler messagesToUserHandler)
   {
      this.messagesToUserHandler = messagesToUserHandler;
      return this;
   }

   public IMessageControllerBuilder AddMessageService(IMessageServiceUseCase messageService)
   {
      this.messageService = messageService;
      return this;
   }

   public MessageControllerOption Build()
   {
      if (messageService is null)
      {
         NullCheck.ErrorIfNull(createMessageHandler);
         NullCheck.ErrorIfNull(conversationHandler);
         NullCheck.ErrorIfNull(messagesToUserHandler);
         messageService = new MessageService(createMessageHandler, conversationHandler, messagesToUserHandler);
      }
      return new MessageControllerOption()
      {
         MessageService = messageService,
      };
   }
}