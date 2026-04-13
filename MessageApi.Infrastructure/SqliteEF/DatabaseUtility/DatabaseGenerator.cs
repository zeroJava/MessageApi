using MessageApi.Domain;

namespace MessageApi.Infrastructure.Sqlite;

public abstract class DatabaseGenerator
{
   public void Check()
   {
      CheckDatabase();
   }

   void CheckDatabase()
   {
      Helper.CheckOperation(CheckDatabaseOperation,
         "Could not find database");
   }

   protected abstract bool CheckDatabaseOperation();

   private static class Helper
   {
      public static void CheckOperation(Func<bool> func, string errorMessage)
      {
         if (!func.Invoke())
         {
            throw new SchemaException(errorMessage);
         }
      }
   }
}