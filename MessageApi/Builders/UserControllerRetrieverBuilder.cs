using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi;

public class UserControllerRetrieverBuilder : IUserControllerRerieverBuilder
{
   readonly string defaultDbOption;

   IUserRepository? userRepository;
   IUserRetriever? userRetriever;
   IInputValidator<string>? inputValidator;
   IRetrieveUserUseCase? retrieveUser;

   public UserControllerRetrieverBuilder(string defaultDbOption)
   {
      this.defaultDbOption = defaultDbOption;
   }

   public IUserControllerRerieverBuilder AddUserRepository(IUserRepository userRepository)
   {
      this.userRepository = userRepository;
      return this;
   }

   public IUserControllerRerieverBuilder AddUserRetriever(IUserRetriever userRetriever)
   {
      this.userRetriever = userRetriever;
      return this;
   }

   public IUserControllerRerieverBuilder AddInputValidator(IInputValidator<string> inputValidator)
   {
      this.inputValidator = inputValidator;
      return this;
   }

   public IUserControllerRerieverBuilder AddRetrieveUserUseCase(IRetrieveUserUseCase retrieveUser)
   {
      this.retrieveUser = retrieveUser;
      return this;
   }

   public UserControllerRetrieverOption Build()
   {
      IUserRepository repository = userRepository ?? UserRepoFactory.GetRepository(defaultDbOption);
      IUserRetriever retriever = userRetriever ?? new UserRetriever(repository);
      IInputValidator<string>? inptValidtr = inputValidator ?? new InputValidator();
      IRetrieveUserUseCase retrieveUserL = retrieveUser ?? new RetrieveUserService(retriever, inptValidtr);
      return new UserControllerRetrieverOption
      {
         UserRepository = repository,
         UserRetriever = retriever,
         InputValidator = inptValidtr,
         RetrieveUser = retrieveUserL,
      };
   }
}