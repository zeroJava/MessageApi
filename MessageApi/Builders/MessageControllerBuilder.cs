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
      ApplicationException error(string name)
      {
         return new ApplicationException($"Property: {name} was not initialised");
      }
      if (messageService is null)
      {
         ICreateMessageHandler createMessageHndlr = createMessageHandler ?? throw error(nameof(createMessageHandler));
         IGetConversationHandler conversationHndlr = conversationHandler ?? throw error(nameof(conversationHandler));
         IGetMessagesToUserHandler messagesToUserHndlr = messagesToUserHandler ?? throw error(nameof(messagesToUserHandler));
         messageService = new MessageService(createMessageHndlr, conversationHndlr, messagesToUserHndlr);
      }
      return new MessageControllerOption()
      {
         MessageService = messageService ?? throw error(nameof(messageService)),
      };
   }
}