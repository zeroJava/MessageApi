namespace MessageApi.Application;

public class UserAuthenticationService : IUserAuthenticationServiceUseCase
{
   readonly IUserAuthenticationHandler userAuthenticationHandler;
   readonly ITokenGenerator tokenGenerator;

   public UserAuthenticationService(IUserAuthenticationHandler userAuthenticationHandler, ITokenGenerator tokenGenerator)
   {
      this.userAuthenticationHandler = userAuthenticationHandler;
      this.tokenGenerator = tokenGenerator;
   }

   public async Task<AuthToken> AuthenticateUser(AuthenticationRequest request)
   {
      await userAuthenticationHandler.Handle(request).ConfigureAwait(false);
      string token = tokenGenerator.GenerateToken(request.Username, "standard");
      return new()
      {
         UserName = request.Username,
         Token = token,
      };
   }
}