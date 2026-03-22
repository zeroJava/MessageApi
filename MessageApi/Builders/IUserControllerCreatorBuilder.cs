using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi;

public interface IUserControllerCreatorBuilder
{
   IUserControllerCreatorBuilder AddUserRepository(IUserRepository repository);
   IUserControllerCreatorBuilder AddUserCreator(IUserCreator creator);
   IUserControllerCreatorBuilder AddNewUserUseCase(INewUserUseCase userUseCase);
   IUserControllerCreatorBuilder AddUserFieldValidator(UserFieldValidatorBase fieldValidator);
   public UserControllerCreatorOption Build();
}

public sealed class UserControllerCreatorOption
{
   public required IUserRepository UserRepository { get; set; }
   public required IUserCreator UserCreator { get; set; }
   public required INewUserUseCase NewUserService { get; set; }
   public required UserFieldValidatorBase UserFieldValidator { get; set; }
}