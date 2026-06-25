using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi;

public class UserControllerCreatorBuilder : IUserControllerCreatorBuilder
{
   readonly string defaultDbOption;

   IUserRepository? userRepository;
   ICreateUserHandler? userCreator;
   IUserServiceUseCase? newUserCreater;
   UserFieldValidatorBase? userFieldValidator;

   public UserControllerCreatorBuilder(string defauultDbOption)
   {
      this.defaultDbOption = defauultDbOption;
   }

   public IUserControllerCreatorBuilder AddNewUserUseCase(IUserServiceUseCase userUseCase)
   {
      this.newUserCreater = userUseCase;
      return this;
   }

   public IUserControllerCreatorBuilder AddUserCreator(ICreateUserHandler creator)
   {
      this.userCreator = creator;
      return this;
   }

   public IUserControllerCreatorBuilder AddUserFieldValidator(UserFieldValidatorBase fieldValidator)
   {
      this.userFieldValidator = fieldValidator;
      return this;
   }

   public IUserControllerCreatorBuilder AddUserRepository(IUserRepository repository)
   {
      this.userRepository = repository;
      return this;
   }

   public UserControllerCreatorOption Build()
   {
      IUserRepository repository = userRepository ?? UserRepoFactory.GetRepository(defaultDbOption);
      ICreateUserHandler creator = userCreator ?? new CreateUserHandler(repository);
      UserFieldValidatorBase fieldValidator = userFieldValidator ?? new UserFieldValidator(repository);
      IUserServiceUseCase newUserUseCase = newUserCreater ?? new UserService(creator, fieldValidator);
      return new UserControllerCreatorOption()
      {
         UserRepository = repository,
         UserCreator = creator,
         NewUserService = newUserUseCase,
         UserFieldValidator = fieldValidator,
      };
   }
}