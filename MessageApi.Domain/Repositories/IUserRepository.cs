namespace MessageApi.Domain;

public interface IUserRepository : IDisposable
{
   void InsertUser(User user);
   void UpdateUser(User user);
   void DeleteUser(User user);

   User? GetUserMatchingId(long userId);
   User? GetUserMatchingUsername(string username);

   bool UsernameAlreadyExist(string username);
   bool EmailAlreadyExist(string email);
}