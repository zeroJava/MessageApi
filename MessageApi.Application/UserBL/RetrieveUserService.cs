using MessageApi.Domain;

namespace MessageApi.Application;

public class RetrieveUserService : IRetrieveUserUseCase
{
   readonly IUserRetriever userRetriever;
   readonly IInputValidator<string> inputValidator;

   public RetrieveUserService(IUserRetriever userRetriever, IInputValidator<string> inputValidator)
   {
      this.userRetriever = userRetriever;
      this.inputValidator = inputValidator;
   }

   public async Task<UserDto?> GetUser(AuthToken token, string username)
   {
      await inputValidator.ValidateAsync(username).ConfigureAwait(false);
      return userRetriever.GetUser(username);
   }
}