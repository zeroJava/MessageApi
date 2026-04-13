using MessageApi.Application;
using MessageApi.Domain;
using MessageApi.Infrastructure.Sqlite;

namespace MessageApi;

public static class UserRepoFactory
{
   public static IUserRepository GetRepository(string option)
   {
      switch (option)
      {
         case "sqlite": return new UserRepository();
         default:
            throw new FactoryException($"{option} is not supported in {nameof(UserRepoFactory)}"); ;
      }
   }
}