using MessageApi.Domain;

namespace MessageApi.Application;

public interface IUserAuthenticator
{
   Task<AuthToken> AuthenticateUser(AuthenticationRequest request);
}

public class UserAuthenticator : IUserAuthenticator
{
   readonly ITokenGenerator tokenGenerator;
   readonly IUserRepository userRepository;
   readonly AuthenticationFieldValidatorBase authenticationFieldValidator;

   public UserAuthenticator(ITokenGenerator tokenGenerator, IUserRepository userRepository, AuthenticationFieldValidatorBase authenticationFieldValidator)
   {
      this.tokenGenerator = tokenGenerator;
      this.userRepository = userRepository;
      this.authenticationFieldValidator = authenticationFieldValidator;
   }

   public async Task<AuthToken> AuthenticateUser(AuthenticationRequest request)
   {
      await authenticationFieldValidator.ValidateFieldAsync(request).ConfigureAwait(false);
      User? user = userRepository.GetUserMatchingUsername(request.Username);
      await authenticationFieldValidator.ValidateAsync(request, user).ConfigureAwait(false);
      string token = tokenGenerator.GenerateToken(request.Username, "standard");
      return new()
      {
         UserName = request.Username,
         Token = token,
      };
   }
}