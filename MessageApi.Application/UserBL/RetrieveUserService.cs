using MessageApi.Domain;

namespace MessageApi.Application;

public class RetrieveUserService : IRetrieveUserUseCase
{
   readonly IUserRetriever userRetriever;
   readonly ITokenValidator tokenValidator;
   readonly IInputValidator<string> inputValidator;

   public RetrieveUserService(IUserRetriever userRetriever, ITokenValidator tokenValidator, IInputValidator<string> inputValidator)
   {
      this.userRetriever = userRetriever;
      this.tokenValidator = tokenValidator;
      this.inputValidator = inputValidator;
   }

   public async Task<UserDto?> GetUser(AuthToken token, string username)
   {
      await tokenValidator.AuthenticateAsync(token).ConfigureAwait(false);
      await inputValidator.ValidateAsync(username).ConfigureAwait(false);
      return userRetriever.GetUser(username);
   }
}