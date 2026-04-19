using MessageApi.Application;

namespace MessageApi.Controllers;

public static class AuthenticationControllerLogic
{
   public static async Task<AuthToken> AuthenticateUser(AuthenticationRequest request, ITokenGenerator tokenGenerator)
   {
      UserAuthenticator userAuthenticator = new(tokenGenerator);
      IUserAuthenticatorUseCase userAuthenticationService = new UserAuthenticatorService(userAuthenticator);
      return await userAuthenticationService.AuthenticateUser(request).ConfigureAwait(false);
   }
}