using MessageApi.Domain;

namespace MessageApi.Application;

internal class MessageInfoMapper : MapperBase<MessageDispatch, MessageInfo>
{
   public override MessageInfo? Map(MessageDispatch? entity)
   {
      if (entity == null)
      {
         return null;
      }
      Message message = entity.Message!;
      return new MessageInfo()
      {
         MessageId = message.Id,
         DispatchId = entity.Id,
         MessageReceivedDate = entity.MessageReceivedTime,
         MessageSentDate = message.MessageCreated,
         SenderName = message.SenderEmailAddress,
         ReceiverName = entity.EmailAddress,
         MessageReceived = entity.MessageReceived,
         MessageContent = message.MessageText,
      };
   }

   public MessageInfo? Map(MessageDispatch? entity, long userId)
   {
      if (entity == null)
      {
         return null;
      }
      Message message = entity.Message!;
      return new MessageInfo()
      {
         MessageId = message.Id,
         DispatchId = entity.Id,
         MessageReceivedDate = entity.MessageReceivedTime,
         MessageSentDate = message.MessageCreated,
         SenderName = message.SenderEmailAddress,
         ReceiverName = entity.EmailAddress,
         MessageReceived = entity.MessageReceived,
         MessageContent = message.MessageText,
         SenderCurrentUser = message.SenderId == userId
      };
   }
}