namespace MessageApi.Application;

public interface IUserAuthenticationServiceUseCase
{
   Task<AuthToken> AuthenticateUser(AuthenticationRequest request);
}