using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi;

public interface IUserControllerCreatorBuilder
{
   IUserControllerCreatorBuilder AddUserRepository(IUserRepository repository);
   IUserControllerCreatorBuilder AddUserCreator(ICreateUserHandler creator);
   IUserControllerCreatorBuilder AddNewUserUseCase(IUserServiceUseCase userUseCase);
   IUserControllerCreatorBuilder AddUserFieldValidator(UserFieldValidatorBase fieldValidator);
   public UserControllerCreatorOption Build();
}

public sealed class UserControllerCreatorOption
{
   public required IUserRepository UserRepository { get; set; }
   public required ICreateUserHandler UserCreator { get; set; }
   public required IUserServiceUseCase NewUserService { get; set; }
   public required UserFieldValidatorBase UserFieldValidator { get; set; }
}