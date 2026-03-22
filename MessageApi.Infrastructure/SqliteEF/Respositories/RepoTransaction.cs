using MessageApi.Domain;
using System.Data;

namespace MessageApi.Infastructure.Sqlite;

public class RepoTransaction : IRepoTransaction
{
   public bool TransactionInvoked => throw new NotImplementedException();

   public IDbConnection DbConnection => throw new NotImplementedException();

   public IDbTransaction DbTransaction => throw new NotImplementedException();

   public void BeginTransaction()
   {
      throw new NotImplementedException();
   }

   public void Callback()
   {
      throw new NotImplementedException();
   }

   public void Callback(string transactionName)
   {
      throw new NotImplementedException();
   }

   public void Commit()
   {
      throw new NotImplementedException();
   }

   public void Dispose()
   {
      throw new NotImplementedException();
   }
}