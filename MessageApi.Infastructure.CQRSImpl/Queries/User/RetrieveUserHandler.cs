using MessageApi.Domain;

namespace MessageApi.Application;

public class RetrieveUserHandler : IRetrieveUserHandler
{
   readonly IUserRepository userRepository;
   readonly UserMapper userMapper = new();

   public RetrieveUserHandler(IUserRepository userRepository)
   {
      this.userRepository = userRepository;
   }

   public async Task<UserDto?> Handle(string username)
   {
      User? user = userRepository.GetUserMatchingUsername(username);
      return userMapper.Map(user);
   }
}