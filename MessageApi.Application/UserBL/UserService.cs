using MessageApi.Domain;

namespace MessageApi.Application;

public class UserService : IUserServiceUseCase
{
   readonly ICreateUserHandler createUserHandler;
   readonly IRetrieveUserHandler retrieveUserHandler;
   readonly IInputValidator<string> inputValidator;
   readonly UserFieldValidatorBase userFieldValidator;

   public UserService(ICreateUserHandler createUserHandler, IRetrieveUserHandler retrieveUserHandler, UserFieldValidatorBase userFieldValidator,
      IInputValidator<string> inputValidator)
   {
      this.createUserHandler = createUserHandler;
      this.retrieveUserHandler = retrieveUserHandler;
      this.userFieldValidator = userFieldValidator;
      this.inputValidator = inputValidator;
   }

   public async Task<NewUserResponse> Create(NewUserData newuser)
   {
      await userFieldValidator.ValidateAsync(newuser).ConfigureAwait(false);
      return await createUserHandler.Handle(newuser).ConfigureAwait(false);
   }

   public async Task<UserDto?> GetUser(AuthToken token, string username)
   {
      await inputValidator.ValidateAsync(username).ConfigureAwait(false);
      return await retrieveUserHandler.Handle(username);
   }
}