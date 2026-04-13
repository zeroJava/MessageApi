using MessageApi.Domain;

namespace MessageApi.Infrastructure.Sqlite;

public class MessageDispatchRepository : IMessageDispatchRepository
{
   public void DeleteDispatch(MessageDispatch dispatch)
   {
      using MessageDbContext dbContext = new();
      dbContext.MessageDispatches.Remove(dispatch);
      dbContext.SaveChanges();
   }

   public void Dispose()
   {
   }

   public MessageDispatch? GetDispatch(long dispatchId)
   {
      using MessageDbContext dbContext = new();
      return dbContext.MessageDispatches.SingleOrDefault(m => m.Id == dispatchId);
   }

   public List<MessageDispatch> GetDispatchesEmail(string email)
   {
      using MessageDbContext dbContext = new();
      return dbContext.MessageDispatches.Where(m => m.EmailAddress == email).ToList();
   }

   public List<MessageDispatch> GetDispatchesMessageId(long messageId)
   {
      using MessageDbContext dbContext = new();
      return dbContext.MessageDispatches.Where(m => m.MessageId == messageId).ToList();
   }

   public List<MessageDispatch> GetDispatchesNotReceived(string email)
   {
      using MessageDbContext dbContext = new();
      return dbContext.MessageDispatches.Where(m => m.EmailAddress == email)
         .Where(m => m.MessageReceived != true).ToList();
   }

   public List<MessageDispatch> GetDispatchesSenderReceiver(string senderEmailAddress, string receiverEmailAddress, long messageIdThreshold, int numberOfMessages)
   {
      return Enumerable.Empty<MessageDispatch>().ToList();
   }

   public void InsertDispatch(MessageDispatch dispatch)
   {
      using MessageDbContext dbContext = new();
      dbContext.MessageDispatches.Add(dispatch);
      dbContext.SaveChanges();
   }

   public void UpdateDispatch(MessageDispatch dispatch)
   {
      using MessageDbContext dbContext = new();
      dbContext.MessageDispatches.Update(dispatch);
      dbContext.SaveChanges();
   }
}