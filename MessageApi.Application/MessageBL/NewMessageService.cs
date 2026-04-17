namespace MessageApi.Application;

public class NewMessageService : IMessageCreatorUseCase
{
   readonly IMessageCreator messageCreator;

   public NewMessageService(IMessageCreator messageCreator)
   {
      this.messageCreator = messageCreator;
   }

   public async Task<MessageRequestState> Create(MessageRequest request)
   {
      return messageCreator.Create(request);
   }
}