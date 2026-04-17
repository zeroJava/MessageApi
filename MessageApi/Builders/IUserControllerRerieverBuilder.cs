using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi;

public interface IUserControllerRerieverBuilder
{
   IUserControllerRerieverBuilder AddUserRepository(IUserRepository userRepository);
   IUserControllerRerieverBuilder AddUserRetriever(IUserRetriever userRetriever);
   IUserControllerRerieverBuilder AddInputValidator(IInputValidator<string> inputValidator);
   IUserControllerRerieverBuilder AddRetrieveUserUseCase(IRetrieveUserUseCase retrieveUser);
   UserControllerRetrieverOption Build();
}

public sealed class UserControllerRetrieverOption
{
   public required IUserRepository UserRepository { get; set; }
   public required IUserRetriever UserRetriever { get; set; }
   public required IInputValidator<string> InputValidator { get; set; }
   public required IRetrieveUserUseCase RetrieveUser { get; set; }
}