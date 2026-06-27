using MessageApi.Application;

namespace MessageApi;

public interface IAuthenticationControllerBuilder
{
   IAuthenticationControllerBuilder AddTokenGenerator(ITokenGenerator tokenGenerator);
   IAuthenticationControllerBuilder AddUserAuthenticationHandler(IUserAuthenticationHandler authenticationHandler);
   IAuthenticationControllerBuilder AddUserAuthenticationService(IUserAuthenticationServiceUseCase authenticationService);
   AuthenticationControllerOption Build();
}

public class AuthenticationControllerBuilder : IAuthenticationControllerBuilder
{
   IUserAuthenticationHandler? authenticationHandler;
   ITokenGenerator? tokenGenerator;
   IUserAuthenticationServiceUseCase? authenticationService;

   public IAuthenticationControllerBuilder AddUserAuthenticationHandler(IUserAuthenticationHandler authenticationHandler)
   {
      this.authenticationHandler = authenticationHandler;
      return this;
   }

   public IAuthenticationControllerBuilder AddTokenGenerator(ITokenGenerator tokenGenerator)
   {
      this.tokenGenerator = tokenGenerator;
      return this;
   }

   public IAuthenticationControllerBuilder AddUserAuthenticationService(IUserAuthenticationServiceUseCase authenticationService)
   {
      this.authenticationService = authenticationService;
      return this;
   }

   public AuthenticationControllerOption Build()
   {
      if (authenticationService is null)
      {
         NullCheck.ErrorIfNull(authenticationHandler);
         NullCheck.ErrorIfNull(tokenGenerator);
         authenticationService = new UserAuthenticationService(authenticationHandler, tokenGenerator);
      }
      return new()
      {
         UserAuthenticationService = authenticationService,
      };
   }
}

public sealed class AuthenticationControllerOption
{
   public required IUserAuthenticationServiceUseCase UserAuthenticationService { get; set; }
}