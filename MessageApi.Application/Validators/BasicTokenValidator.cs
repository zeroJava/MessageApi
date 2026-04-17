using MessageApi.Domain;

namespace MessageApi.Application;

public interface ITokenValidator
{
   void Authenticate(AuthToken token);
   Task AuthenticateAsync(AuthToken token);
}