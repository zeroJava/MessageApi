using MessageApi.Domain;

namespace MessageApi.Application;

public interface IUserAuthenticator
{
   Task AuthenticateUser(AuthenticationRequest request);
}

public class UserAuthenticator : IUserAuthenticator
{
   readonly IUserRepository userRepository;
   readonly AuthenticationFieldValidatorBase authenticationFieldValidator;

   public UserAuthenticator(IUserRepository userRepository, AuthenticationFieldValidatorBase authenticationFieldValidator)
   {
      this.userRepository = userRepository;
      this.authenticationFieldValidator = authenticationFieldValidator;
   }

   public async Task AuthenticateUser(AuthenticationRequest request)
   {
      await authenticationFieldValidator.ValidateFieldAsync(request).ConfigureAwait(false);
      User? user = userRepository.GetUserMatchingUsername(request.Username);
      await authenticationFieldValidator.ValidateAsync(request, user).ConfigureAwait(false);
   }
}