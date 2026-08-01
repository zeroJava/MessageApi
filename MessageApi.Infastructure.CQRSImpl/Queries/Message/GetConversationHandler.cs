using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi.Infrastructure.CQRSImpl;

public class GetConversationHandler : IGetConversationHandler
{
   readonly IUserRepository userRepository;
   readonly IMessageDispatchRepository messageDispatchRepository;
   readonly MessageInfoMapper mapper = new();
   readonly Helper helper = new();

   public GetConversationHandler(IUserRepository userRepository, IMessageDispatchRepository messageDispatchRepository)
   {
      this.userRepository = userRepository;
      this.messageDispatchRepository = messageDispatchRepository;
   }

   public async Task<IEnumerable<MessageInfo>> Handle(ConversationRequest request)
   {
      User user = UserHelper.GetUser(userRepository, request.Username);
      IEnumerable<MessageInfo> postedMessages = GetDispathces(request, user);
      return postedMessages;
   }

   IEnumerable<MessageInfo> GetDispathces(ConversationRequest request, User user)
   {
      IEnumerable<MessageDispatch> dispatches = messageDispatchRepository.GetDispatchesSenderReceiver(request.SenderEmailAddress,
         request.ReceiverEmailAddress,
         request.MessageIdThreshold,
         request.NumberOfMessages);
      IEnumerable<MessageInfo> dispatchInfos = helper.GetDispatchInfo(dispatches, user.Id, mapper);
      return dispatchInfos;
   }

   class Helper
   {
      public IEnumerable<MessageInfo> GetDispatchInfo(IEnumerable<MessageDispatch> dispatches, long userId,
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