using MessageApi.Application;

namespace MessageApi;

public interface IMessageControllerBuilder
{
   IMessageControllerBuilder AddMessageService(IMessageServiceUseCase messageService);
   MessageControllerOption Build();
}

public sealed record MessageControllerOption
{
   public required IMessageServiceUseCase MessageService { get; set; }
}

public class MessageControllerBuilder : IMessageControllerBuilder
{
   IMessageServiceUseCase? messageService { get; set; }

   public MessageControllerBuilder()
   {
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
      IMessageServiceUseCase messageServiceToUse = messageService ?? throw error(nameof(messageService));
      return new MessageControllerOption()
      {
         MessageService = messageServiceToUse,
      };
   }
}