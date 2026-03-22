using MessageApi.Domain;

namespace MessageApi.Application;

public interface IUserCreator
{
   Task<UserDto> CreateNewUser(NewUserData newuser);
}

public class UserCreator : IUserCreator
{
   readonly IUserRepository userRepository;
   readonly UserMapper userMapper = new();

   public UserCreator(IUserRepository userRepository)
   {
      this.userRepository = userRepository;
   }

   public async Task<UserDto> CreateNewUser(NewUserData newuser)
   {
      User user = Helper.Extract(newuser);
      userRepository.InsertUser(user);
      return userMapper.Map(user)!;
   }

   class Helper
   {
      public static User Extract(NewUserData newuser)
      {
         return new()
         {
            Id = default,
            UserName = newuser.UserName,
            Password = PasswordGenerator.Generate(newuser.Password),
            FirstName = newuser.FirstName,
            Surname = newuser.Surname,
            DOB = newuser.Dob,
            EmailAddress = newuser.EmailAddress,
            Gender = newuser.Gender,
         };
      }
   }
}