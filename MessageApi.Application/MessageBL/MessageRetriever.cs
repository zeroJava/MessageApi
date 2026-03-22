using MessageApi.Domain;

namespace MessageApi.Application;

public interface IMessageRetriever
{
   List<MessageInfo> GetMessagesSentToUser(RetrieveMessageRequest messageRequest);
   List<MessageInfo> GetConversation(RetrieveMessageRequest messageRequest);
}

public class MessageRetriever : IMessageRetriever
{
   readonly IUserRepository userRepository;
   readonly IMessageRepository messageRepository;
   readonly IMessageDispatchRepository messageDispatchRepository;
   readonly MessageInfoMapper mapper = new();
   readonly Helper helper = new();

   public MessageRetriever(IUserRepository userRepository, IMessageRepository messageRepository, IMessageDispatchRepository messageDispatchRepository)
   {
      this.userRepository = userRepository;
      this.messageRepository = messageRepository;
      this.messageDispatchRepository = messageDispatchRepository;
   }

   public List<MessageInfo> GetMessagesSentToUser(RetrieveMessageRequest messageRequest)
   {
      User user = UserHelper.GetUser(userRepository, messageRequest.Username);
      return GetMessagesSent(user.Id, messageRequest.ReceiverEmailAddress);
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

   public List<MessageInfo> GetConversation(RetrieveMessageRequest messageRequest)
   {
      User user = UserHelper.GetUser(userRepository, messageRequest.Username);
      List<MessageInfo> postedMessages = GetDispathces(messageRequest, user);
      return postedMessages;
   }

   List<MessageInfo> GetDispathces(RetrieveMessageRequest request, User user)
   {
      List<MessageDispatch> dispatches = messageDispatchRepository.GetDispatchesSenderReceiver(request.SenderEmailAddress,
         request.ReceiverEmailAddress,
         request.MessageIdThreshold,
         request.NumberOfMessages);
      List<MessageInfo> dispatchInfos = helper.GetDispatchInfo(dispatches, user.Id, mapper);
      return dispatchInfos;
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