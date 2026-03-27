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
   }

   public void Callback()
   {
   }

   public void Callback(string transactionName)
   {
   }

   public void Commit()
   {
   }

   public void Dispose()
   {
   }
}