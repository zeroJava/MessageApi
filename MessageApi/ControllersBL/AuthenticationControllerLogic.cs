using MessageApi.Application;

namespace MessageApi.Controllers;

public static class AuthenticationControllerLogic
{
   public static async Task<AuthToken> AuthenticateUser(AuthenticationRequest request, IAuthenticationControllerBuilder authenticationControllerBuilder)
   {
      AuthenticationControllerOption option = authenticationControllerBuilder.Build();
      UserAuthenticator userAuthenticator = new(option.TokenGenerator, option.UserRepository, option.AuthenticationFieldValidator);
      IUserAuthenticatorUseCase userAuthenticationService = new UserAuthenticatorService(userAuthenticator);
      return await userAuthenticationService.AuthenticateUser(request).ConfigureAwait(false);
   }
}