namespace MessageApi.Domain;

public interface IMessageDispatchRepository : IDisposable
{
   void InsertDispatch(MessageDispatch dispatch);
   void UpdateDispatch(MessageDispatch dispatch); // Tuple<string, IDbDataParameter[]> query where TParameter : IDbDataParameter;
   void DeleteDispatch(MessageDispatch dispatch);

   MessageDispatch? GetDispatch(long dispatchId);
   List<MessageDispatch> GetDispatchesMessageId(long messageId);
   List<MessageDispatch> GetDispatchesEmail(string email);
   List<MessageDispatch> GetDispatchesNotReceived(string email);
   List<MessageDispatch> GetDispatchesSenderReceiver(string senderEmailAddress, string receiverEmailAddress,
       long messageIdThreshold,
       int numberOfMessages);
}