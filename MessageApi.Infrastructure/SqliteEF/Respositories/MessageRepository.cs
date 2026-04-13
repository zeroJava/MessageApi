using MessageApi.Domain;

namespace MessageApi.Infrastructure.Sqlite;

public class MessageRepository : IMessageRepository
{
   public void DeleteMessage(Message message)
   {
      using MessageDbContext dbContext = new();
      dbContext.Messages.Remove(message);
      dbContext.SaveChanges();
   }

   public void Dispose()
   {
   }

   public Message? GetMessage(long messageId)
   {
      using MessageDbContext dbContext = new();
      return dbContext.Messages.SingleOrDefault(m => m.Id == messageId);
   }

   public List<Message> GetMessages(IEnumerable<long> messageids)
   {
      using MessageDbContext dbContext = new();
      return dbContext.Messages.Where(m => messageids.Any(i => i == m.Id)).ToList();
   }

   public List<Message> GetMessagesMatchingText(string text)
   {
      using MessageDbContext dbContext = new();
      return dbContext.Messages.Where(m => m.MessageText == text).ToList();
   }

   public void InsertMessage(Message message)
   {
      using MessageDbContext dbContext = new();
      dbContext.Messages.Add(message);
      dbContext.SaveChanges();
   }

   public void UpdateMessage(Message message)
   {
      using MessageDbContext dbContext = new();
      dbContext.Messages.Update(message);
      dbContext.SaveChanges();
   }
}