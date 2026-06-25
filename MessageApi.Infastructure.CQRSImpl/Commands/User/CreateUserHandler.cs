using MessageApi.Domain;

namespace MessageApi.Application;

public class CreateUserHandler : ICreateUserHandler
{
   readonly IUserRepository userRepository;

   public CreateUserHandler(IUserRepository userRepository)
   {
      this.userRepository = userRepository;
   }

   public async Task<NewUserResponse> Handle(NewUserData newuser)
   {
      User user = Helper.Extract(newuser);
      userRepository.InsertUser(user);
      return ResponseMapper.Map(user);
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

   class ResponseMapper
   {
      public static NewUserResponse Map(User? entity)
      {
         return new NewUserResponse()
         {
            Id = entity?.Id ?? 0,
            UserName = entity?.UserName ?? string.Empty,
            State = entity is not null,
         };
      }
   }
}