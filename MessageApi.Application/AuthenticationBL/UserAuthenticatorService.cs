namespace MessageApi.Application;

public class UserAuthenticatorService : IUserAuthenticatorUseCase
{
   readonly IUserAuthenticator userAuthenticator;

   public UserAuthenticatorService(IUserAuthenticator userAuthenticator)
   {
      this.userAuthenticator = userAuthenticator;
   }

   public async Task<AuthToken> AuthenticateUser(AuthenticationRequest request)
   {
      return await userAuthenticator.AuthenticateUser(request).ConfigureAwait(false);
   }
}