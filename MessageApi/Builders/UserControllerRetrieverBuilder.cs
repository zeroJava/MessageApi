using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi;

public class UserControllerRetrieverBuilder : IUserControllerRerieverBuilder
{
   IUserRepository? userRepository;
   IUserRetriever? userRetriever;
   ITokenValidator? tokenValidator;
   IInputValidator<string>? inputValidator;
   IRetrieveUserUseCase? retrieveUser;

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

   public IUserControllerRerieverBuilder AddTokenValidator(ITokenValidator tokenValidator)
   {
      this.tokenValidator = tokenValidator;
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
      IUserRepository repository = userRepository ?? UserRepoFactory.GetRepository("sqlite");
      IUserRetriever retriever = userRetriever ?? new UserRetriever(repository);
      ITokenValidator toknValidtr = tokenValidator ?? new TokenValidator(repository);
      IInputValidator<string>? inptValidtr = inputValidator ?? new InputValidator();
      IRetrieveUserUseCase retrieveUserL = retrieveUser ?? new RetrieveUserService(retriever, toknValidtr, inptValidtr);
      return new UserControllerRetrieverOption
      {
         UserRepository = repository,
         UserRetriever = retriever,
         TokenValidator = toknValidtr,
         InputValidator = inptValidtr,
         RetrieveUser = retrieveUserL,
      };
   }
}