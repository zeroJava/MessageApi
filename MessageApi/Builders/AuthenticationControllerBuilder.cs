using MessageApi.Application;
using MessageApi.Domain;
using MessageApi.Infrastructure;

namespace MessageApi;

public class AuthenticationControllerBuilder : IAuthenticationControllerBuilder
{
   readonly string defaultDbOption;

   AuthenticationFieldValidatorBase? authenticationFieldValidator;
   IUserRepository? userRepository;
   ITokenGenerator? tokenGenerator;

   public AuthenticationControllerBuilder(string defaultDbOption)
   {
      this.defaultDbOption = defaultDbOption;
   }

   public IAuthenticationControllerBuilder AddAuthenticationFieldValidator(AuthenticationFieldValidatorBase authenticationFieldValidator)
   {
      this.authenticationFieldValidator = authenticationFieldValidator;
      return this;
   }

   public IAuthenticationControllerBuilder AddTokenGenerator(ITokenGenerator tokenGenerator)
   {
      this.tokenGenerator = tokenGenerator;
      return this;
   }

   public IAuthenticationControllerBuilder AddUserRepository(IUserRepository userRepository)
   {
      this.userRepository = userRepository;
      return this;
   }

   public AuthenticationControllerOption Build()
   {
      AuthenticationFieldValidatorBase authValidator = authenticationFieldValidator ?? new AuthenticationFieldValidator();
      ITokenGenerator tokenGen = tokenGenerator ?? new SimpleJwtTokenGenerator();
      IUserRepository userRepo = userRepository ?? UserRepoFactory.GetRepository(defaultDbOption);
      return new()
      {
         AuthenticationFieldValidator = authValidator,
         TokenGenerator = tokenGen,
         UserRepository = userRepo,
      };
   }
}