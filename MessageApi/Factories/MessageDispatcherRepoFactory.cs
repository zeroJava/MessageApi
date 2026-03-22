using MessageApi.Application;
using MessageApi.Domain;
using MessageApi.Infastructure.Sqlite;

namespace MessageApi;

public static class MessageDispatcherRepoFactory
{
   public static IMessageDispatchRepository GetRepository(string option)
   {
      switch (option)
      {
         case "sqlite": return new MessageDispatchRepository();
         default:
            throw new FactoryException($"{option} is not supported in {nameof(MessageDispatcherRepoFactory)}"); ;
      }
   }
}