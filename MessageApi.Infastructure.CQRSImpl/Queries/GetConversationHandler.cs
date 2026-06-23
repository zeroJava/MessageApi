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

   public async Task<List<MessageInfo>> Handle(ConversationRequest request)
   {
      User user = UserHelper.GetUser(userRepository, request.Username);
      List<MessageInfo> postedMessages = GetDispathces(request, user);
      return postedMessages;
   }

   List<MessageInfo> GetDispathces(ConversationRequest request, User user)
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