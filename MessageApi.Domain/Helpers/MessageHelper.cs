namespace MessageApi.Domain;

public static class MessageHelper
{
   public static List<Message> GetMessages(IMessageRepository messageRepository, IEnumerable<MessageDispatch> dispatches)
   {
      IEnumerable<long> messageIds = dispatches.Where(x => x.MessageId != null)
         .Select(x => x.MessageId!.Value)
         .Distinct();
      return messageRepository.GetMessages(messageIds);
   }
}