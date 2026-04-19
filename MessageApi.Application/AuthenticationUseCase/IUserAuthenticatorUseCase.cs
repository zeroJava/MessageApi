namespace MessageApi.Application;

public interface IUserAuthenticatorUseCase
{
   Task<AuthToken> AuthenticateUser(AuthenticationRequest request);
}