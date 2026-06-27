using MessageApi.Domain;

namespace MessageApi.Application;

public class UserAuthenticationHandler : IUserAuthenticationHandler
{
   readonly IUserRepository userRepository;
   readonly AuthenticationFieldValidatorBase authenticationFieldValidator;

   public UserAuthenticationHandler(IUserRepository userRepository, AuthenticationFieldValidatorBase authenticationFieldValidator)
   {
      this.userRepository = userRepository;
      this.authenticationFieldValidator = authenticationFieldValidator;
   }

   public async Task Handle(AuthenticationRequest request)
   {
      await authenticationFieldValidator.ValidateFieldAsync(request).ConfigureAwait(false);
      User? user = userRepository.GetUserMatchingUsername(request.Username);
      await authenticationFieldValidator.ValidateAsync(request, user).ConfigureAwait(false);
   }
}