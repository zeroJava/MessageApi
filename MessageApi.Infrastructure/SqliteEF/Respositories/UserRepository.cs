using MessageApi.Domain;

namespace MessageApi.Infastructure.Sqlite;

public class UserRepository : IUserRepository
{
   public void DeleteUser(User user)
   {
      using MessageDbContext dbContext = new();
      dbContext.Users.Remove(user);
      dbContext.SaveChanges();
   }

   public void Dispose()
   {
   }

   public bool EmailAlreadyExist(string email)
   {
      using MessageDbContext dbContext = new();
      return dbContext.Users.Any(u => u.EmailAddress == email);
   }

   public User? GetUserMatchingId(long userId)
   {
      using MessageDbContext dbContext = new();
      return dbContext.Users.SingleOrDefault(u => u.Id == userId);
   }

   public User? GetUserMatchingUsername(string username)
   {
      using MessageDbContext dbContext = new();
      return dbContext.Users.SingleOrDefault(u => u.UserName == username);
   }

   public void InsertUser(User user)
   {
      using MessageDbContext dbContext = new();
      dbContext.Users.Add(user);
      dbContext.SaveChanges();
   }

   public void UpdateUser(User user)
   {
      using MessageDbContext dbContext = new();
      dbContext.Users.Update(user);
      dbContext.SaveChanges();
   }

   public bool UsernameAlreadyExist(string username)
   {
      using MessageDbContext dbContext = new();
      return dbContext.Users.Any(u => u.UserName == username);
   }
}