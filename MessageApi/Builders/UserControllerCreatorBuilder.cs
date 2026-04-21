using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi;

public class UserControllerCreatorBuilder : IUserControllerCreatorBuilder
{
   readonly string defaultDbOption;

   IUserRepository? userRepository;
   IUserCreator? userCreator;
   INewUserUseCase? newUserCreater;
   UserFieldValidatorBase? userFieldValidator;

   public UserControllerCreatorBuilder(string defauultDbOption)
   {
      this.defaultDbOption = defauultDbOption;
   }

   public IUserControllerCreatorBuilder AddNewUserUseCase(INewUserUseCase userUseCase)
   {
      this.newUserCreater = userUseCase;
      return this;
   }

   public IUserControllerCreatorBuilder AddUserCreator(IUserCreator creator)
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
      IUserCreator creator = userCreator ?? new UserCreator(repository);
      UserFieldValidatorBase fieldValidator = userFieldValidator ?? new UserFieldValidator(repository);
      INewUserUseCase newUserUseCase = newUserCreater ?? new NewUserService(creator, fieldValidator);
      return new UserControllerCreatorOption()
      {
         UserRepository = repository,
         UserCreator = creator,
         NewUserService = newUserUseCase,
         UserFieldValidator = fieldValidator,
      };
   }
}