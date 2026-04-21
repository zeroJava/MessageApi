using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi;

public interface IAuthenticationControllerBuilder
{
   IAuthenticationControllerBuilder AddUserRepository(IUserRepository repository);
   IAuthenticationControllerBuilder AddTokenGenerator(ITokenGenerator tokenGenerator);
   IAuthenticationControllerBuilder AddAuthenticationFieldValidator(AuthenticationFieldValidatorBase authenticationFieldValidator);
   AuthenticationControllerOption Build();
}

public sealed class AuthenticationControllerOption
{
   public required IUserRepository UserRepository { get; set; }
   public required ITokenGenerator TokenGenerator { get; set; }
   public required AuthenticationFieldValidatorBase AuthenticationFieldValidator { get; set; }
}