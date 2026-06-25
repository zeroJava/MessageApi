using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi.Infrastructure.CQRSImpl;

public class CreateMessageHandler : ICreateMessageHandler
{
   static readonly MessageRequestState MessageSuccess = new()
   {
      MessageRecievedState = MessageReceivedState.Successful,
      Message = "Message was successfully acknowledged and stored"
   };

   readonly IUserRepository userRepository;
   readonly IMessageRepository messageRepository;
   readonly IMessageDispatchRepository messageDispatchRepository;
   readonly IRepoTransaction repoTransaction;

   public CreateMessageHandler(IUserRepository userRepository, IMessageRepository messageRepository, IMessageDispatchRepository messageDispatchRepository,
      IRepoTransaction repoTransaction)
   {
      this.userRepository = userRepository;
      this.messageRepository = messageRepository;
      this.messageDispatchRepository = messageDispatchRepository;
      this.repoTransaction = repoTransaction;
   }

   public async Task<MessageRequestState> Handle(CreateMessageRequest request)
   {
      User user = UserHelper.GetUser(userRepository, request.UserName);
      ProcessRequest(request, user);
      return MessageSuccess;
   }

   void ProcessRequest(CreateMessageRequest request, User user)
   {
      try
      {
         repoTransaction.BeginTransaction();
         Message message = CreateMessage(request, user);
         CreateDispatch(request, message);
         repoTransaction.Commit();
      }
      catch
      {
         repoTransaction.Callback();
         throw;
      }
   }

   Message CreateMessage(CreateMessageRequest request, User user)
   {
      Message message = new()
      {
         Id = default,
         MessageText = request.Message,
         SenderId = user.Id,
         SenderEmailAddress = user.EmailAddress,
         MessageCreated = request.MessageCreated
      };
      messageRepository.InsertMessage(message);
      return message;
   }

   void CreateDispatch(CreateMessageRequest request, Message message)
   {
      foreach (string emailAddress in request.EmailAccounts)
      {
         MessageDispatch messageDispatch = new()
         {
            Id = default,
            EmailAddress = emailAddress,
            MessageId = message.Id,
            MessageReceived = false,
         };
         messageDispatchRepository.InsertDispatch(messageDispatch);
      }
   }
}