using MessageApi.Domain;

namespace MessageApi.Application;

public abstract class UserFieldValidatorBase : IInputValidator<NewUserData>
{
   public abstract void Validate(NewUserData newuser);
   public abstract Task ValidateAsync(NewUserData input);
}

public class UserFieldValidator : UserFieldValidatorBase
{
   readonly IUserRepository userRepository;

   public UserFieldValidator(IUserRepository userRepository)
   {
      this.userRepository = userRepository;
   }

   void CheckUsernameAlreadyExist(string username)
   {
      if (userRepository.UsernameAlreadyExist(username))
      {
         throw new UserValidationException($"Username: {username} is alreay exist");
      }
   }

   void CheckEmailAlreadyExist(string email)
   {
      if (userRepository.EmailAlreadyExist(email))
      {
         throw new UserValidationException($"Email address: {email} is alreay exist");
      }
   }

   void CheckDateOfBirthFormat(string dob)
   {
      //Rege
   }

   public static void Check(IUserRepository userRepository, NewUserData newuser)
   {
      UserFieldValidator fieldValidator = new(userRepository);
      fieldValidator.Validate(newuser);
   }

   public override void Validate(NewUserData newuser)
   {
      DoValidation(newuser);
   }

   public override async Task ValidateAsync(NewUserData newuser)
   {
      await Task.Run(() =>
      {
         DoValidation(newuser);
      }).ConfigureAwait(false);
   }

   void DoValidation(NewUserData newuser)
   {
      CheckUsernameAlreadyExist(newuser.UserName);
      CheckEmailAlreadyExist(newuser.EmailAddress);
      CheckDateOfBirthFormat(newuser.Dob.ToString());
   }
}

public class UserValidationException : Exception
{
   public UserValidationException(string message) : base(message)
   {
      //
   }
}