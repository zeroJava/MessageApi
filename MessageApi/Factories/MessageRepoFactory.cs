using MessageApi.Application;
using MessageApi.Domain;
using MessageApi.Infastructure.Sqlite;

namespace MessageApi;

public static class MessageRepoFactory
{
   public static IMessageRepository GetRepository(string option)
   {
      switch (option)
      {
         case "sqlite": return new MessageRepository();
         default:
            throw new FactoryException($"{option} is not supported in {nameof(MessageRepoFactory)}"); ;
      }
   }
}