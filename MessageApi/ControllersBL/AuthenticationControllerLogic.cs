using MessageApi.Application;

namespace MessageApi.Controllers;

public static class AuthenticationControllerLogic
{
   public static async Task<AuthToken> AuthenticateUser(AuthenticationRequest request, IAuthenticationControllerBuilder authenticationControllerBuilder)
   {
      AuthenticationControllerOption option = authenticationControllerBuilder.Build();
      IUserAuthenticationServiceUseCase userAuthenticationService = option.UserAuthenticationService;
      return await userAuthenticationService.AuthenticateUser(request).ConfigureAwait(false);
   }
}