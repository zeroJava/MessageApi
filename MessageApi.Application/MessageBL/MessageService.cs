namespace MessageApi.Application;

public class MessageService : IMessageServiceUseCase
{
   readonly ICreateMessageHandler createMessageHandler;
   readonly IGetConversationHandler getConversationHandler;
   readonly IGetMessagesToUserHandler getMessagesToUserHandler;

   public MessageService(ICreateMessageHandler createMessageHandler, IGetConversationHandler getConversationHandler,
      IGetMessagesToUserHandler getMessagesToUserHandler)
   {
      this.createMessageHandler = createMessageHandler;
      this.getConversationHandler = getConversationHandler;
      this.getMessagesToUserHandler = getMessagesToUserHandler;
   }

   public async Task<MessageRequestState> Create(MessageRequest request)
   {
      // Token and data validation here
      CreateMessageRequest createRequest = new()
      {
         UserName = request.UserName,
         Message = request.Message,
         EmailAccounts = request.EmailAccounts,
         MessageCreated = DateTime.UtcNow,
      };
      return await createMessageHandler.Handle(createRequest);
   }

   public async Task<List<MessageInfo>> GetConversation(RetrieveMessageRequest messageRequest)
   {
      // Token validation here
      ConversationRequest conversationRequest = new()
      {
         MessageIdThreshold = messageRequest.MessageIdThreshold,
         NumberOfMessages = messageRequest.NumberOfMessages,
         ReceiverEmailAddress = messageRequest.ReceiverEmailAddress,
         SenderEmailAddress = messageRequest.SenderEmailAddress,
      };
      return await getConversationHandler.Handle(conversationRequest);
   }

   public async Task<List<MessageInfo>> GetMessagesSentToUser(RetrieveMessageRequest messageRequest)
   {
      // Token validation here
      MessagesToUserRequest request = new()
      {
         SenderEmailAddress = messageRequest.SenderEmailAddress,
      };
      return await getMessagesToUserHandler.Handle(request);
   }
}