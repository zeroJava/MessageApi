using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi.Infrastructure.CQRSImpl;

public class GetMessagesToUserHandler : IGetMessagesToUserHandler
{
   readonly IUserRepository userRepository;
   readonly IMessageRepository messageRepository;
   readonly IMessageDispatchRepository messageDispatchRepository;
   readonly MessageInfoMapper mapper = new();
   readonly Helper helper = new();

   public GetMessagesToUserHandler(IUserRepository userRepository, IMessageRepository messageRepository, IMessageDispatchRepository messageDispatchRepository)
   {
      this.userRepository = userRepository;
      this.messageRepository = messageRepository;
      this.messageDispatchRepository = messageDispatchRepository;
   }

   public async Task<List<MessageInfo>> Handle(MessagesToUserRequest request)
   {
      User user = UserHelper.GetUser(userRepository, request.Username);
      return GetMessagesSent(user.Id, request.ReceiverEmailAddress);
   }

   List<MessageInfo> GetMessagesSent(long userId, string receiverEmail)
   {
      List<MessageDispatch> dispatches = messageDispatchRepository.GetDispatchesNotReceived(receiverEmail);
      IEnumerable<Message> messages = MessageHelper.GetMessages(messageRepository, dispatches);
      foreach (MessageDispatch dispatch in dispatches)
      {
         Message? message = messages.FirstOrDefault(m => m.Id == dispatch.MessageId);
         if (message is not null)
         {
            dispatch.Message = message;
         }
      }
      return helper.GetDispatchInfo(dispatches, userId, mapper);
   }

   class Helper
   {
      public List<MessageInfo> GetDispatchInfo(List<MessageDispatch> dispatches, long userId,
         MessageInfoMapper mapper)
      {
         List<MessageInfo> postedMessageInfo = new();
         foreach (MessageDispatch dispatch in dispatches)
         {
            MessageInfo? info = mapper.Map(dispatch, userId);
            if (info is not null)
            {
               postedMessageInfo.Add(info);
            }
         }
         return postedMessageInfo;
      }
   }
}