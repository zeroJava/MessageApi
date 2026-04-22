namespace MessageApi.Application;

public class UserAuthenticatorService : IUserAuthenticatorUseCase
{
   readonly IUserAuthenticator userAuthenticator;
   readonly ITokenGenerator tokenGenerator;

   public UserAuthenticatorService(IUserAuthenticator userAuthenticator, ITokenGenerator tokenGenerator)
   {
      this.userAuthenticator = userAuthenticator;
      this.tokenGenerator = tokenGenerator;
   }

   public async Task<AuthToken> AuthenticateUser(AuthenticationRequest request)
   {
      await userAuthenticator.AuthenticateUser(request).ConfigureAwait(false);
      string token = tokenGenerator.GenerateToken(request.Username, "standard");
      return new()
      {
         UserName = request.Username,
         Token = token,
      };
   }
}