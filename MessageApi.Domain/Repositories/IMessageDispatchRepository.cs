namespace MessageApi.Domain;

public interface IMessageDispatchRepository : IDisposable
{
   void InsertDispatch(MessageDispatch dispatch);
   void UpdateDispatch(MessageDispatch dispatch); // Tuple<string, IDbDataParameter[]> query where TParameter : IDbDataParameter;
   void DeleteDispatch(MessageDispatch dispatch);

   MessageDispatch? GetDispatch(long dispatchId);
   IEnumerable<MessageDispatch> GetDispatchesMessageId(long messageId);
   IEnumerable<MessageDispatch> GetDispatchesEmail(string email);
   IEnumerable<MessageDispatch> GetDispatchesNotReceived(string email);
   IEnumerable<MessageDispatch> GetDispatchesSenderReceiver(string senderEmailAddress, string receiverEmailAddress,
       long messageIdThreshold,
       int numberOfMessages);
}