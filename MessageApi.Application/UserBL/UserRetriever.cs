using MessageApi.Domain;

namespace MessageApi.Application;

public interface IUserRetriever
{
   UserDto? GetUser(string username);
}

public class UserRetriever : IUserRetriever
{
   readonly IUserRepository userRepository;
   readonly UserMapper userMapper = new();

   public UserRetriever(IUserRepository userRepository)
   {
      this.userRepository = userRepository;
   }

   public UserDto? GetUser(string username)
   {
      User? user = userRepository.GetUserMatchingUsername(username);
      return userMapper.Map(user);
   }
}