namespace MessageApi.Domain;

public static class UserHelper
{
   public static User GetUser(IUserRepository userRepository, string userName)
   {
      User? user = userRepository.GetUserMatchingUsername(userName);
      return user ??
         throw new ApplicationException($"Could not find user matching Username: {userName}.");
   }
}