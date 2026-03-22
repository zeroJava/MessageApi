namespace MessageApi.Application;

public class NewUserService : INewUserUseCase
{
   readonly IUserCreator userCreator;
   readonly UserFieldValidatorBase userFieldValidator;

   public NewUserService(IUserCreator userCreator, UserFieldValidatorBase userFieldValidator)
   {
      this.userCreator = userCreator;
      this.userFieldValidator = userFieldValidator;
   }

   public async Task<UserDto> Create(NewUserData newuser)
   {
      await userFieldValidator.ValidateAsync(newuser).ConfigureAwait(false);
      return await userCreator.CreateNewUser(newuser).ConfigureAwait(false);
   }
}