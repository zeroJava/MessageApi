namespace MessageApi.Application;

public interface IUserAuthenticator
{
   Task<AuthToken> AuthenticateUser(AuthenticationRequest request);
}

public class UserAuthenticator : IUserAuthenticator
{
   readonly ITokenGenerator tokenGenerator;

   public UserAuthenticator(ITokenGenerator tokenService)
   {
      this.tokenGenerator = tokenService;
   }

   public async Task<AuthToken> AuthenticateUser(AuthenticationRequest request)
   {
      string token = "undefined";
      if (request.Username == "testuser" && request.Password == "testpassword") // replace
      {
         token = tokenGenerator.GenerateToken(request.Username, "standard");
      }
      return new()
      {
         UserName = request.Username,
         Token = token,
      };
   }
}