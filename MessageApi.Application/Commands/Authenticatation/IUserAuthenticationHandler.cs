namespace MessageApi.Application;

public interface IUserAuthenticationHandler
{
   Task Handle(AuthenticationRequest request);
}