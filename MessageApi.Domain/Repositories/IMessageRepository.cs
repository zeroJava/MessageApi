namespace MessageApi.Domain;

public interface IMessageRepository : IDisposable
{
   void InsertMessage(Message message);
   void UpdateMessage(Message message); // Tuple<string, TParameter[]> query where TParameter : IDbDataParameter;
   void DeleteMessage(Message message);

   Message? GetMessage(long messageId);
   List<Message> GetMessagesMatchingText(string text);
   List<Message> GetMessages(IEnumerable<long> messageids);
}