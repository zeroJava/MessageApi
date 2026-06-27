using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi;

public interface IUserControllerBuilder
{
   IUserControllerBuilder AddUserServiceUseCase(IUserServiceUseCase userService);
   IUserControllerBuilder AddCreateUserHandler(ICreateUserHandler createUserHandler);
   IUserControllerBuilder AddIRetrieveUserHandler(IRetrieveUserHandler retrieveUserHandler);
   IUserControllerBuilder AddUserFieldValidator(UserFieldValidatorBase fieldValidator);
   IUserControllerBuilder AddInputValidator(IInputValidator<string> inputValidator);
   public UserControllerOption Build();
}

public class UserControllerBuilder : IUserControllerBuilder
{
   IUserServiceUseCase? userService;
   ICreateUserHandler? createUserHandler;
   IRetrieveUserHandler? retrieveUserHandler;
   UserFieldValidatorBase? userFieldValidator;
   IInputValidator<string>? inputValidator;

   public IUserControllerBuilder AddCreateUserHandler(ICreateUserHandler createUserHandler)
   {
      this.createUserHandler = createUserHandler;
      return this;
   }

   public IUserControllerBuilder AddInputValidator(IInputValidator<string> inputValidator)
   {
      this.inputValidator = inputValidator;
      return this;
   }

   public IUserControllerBuilder AddIRetrieveUserHandler(IRetrieveUserHandler retrieveUserHandler)
   {
      this.retrieveUserHandler = retrieveUserHandler;
      return this;
   }

   public IUserControllerBuilder AddUserFieldValidator(UserFieldValidatorBase fieldValidator)
   {
      this.userFieldValidator = fieldValidator;
      return this;
   }

   public IUserControllerBuilder AddUserServiceUseCase(IUserServiceUseCase userService)
   {
      this.userService = userService;
      return this;
   }

   public UserControllerOption Build()
   {
      if (userService is null)
      {
         NullCheck.ErrorIfNull(createUserHandler);
         NullCheck.ErrorIfNull(retrieveUserHandler);
         NullCheck.ErrorIfNull(inputValidator);
         NullCheck.ErrorIfNull(userFieldValidator);
         userService = new UserService(createUserHandler, retrieveUserHandler, userFieldValidator, inputValidator);
      }
      return new UserControllerOption()
      {
         UserService = userService,
      };
   }
}

public sealed class UserControllerOption
{
   public required IUserServiceUseCase UserService { get; set; }
}