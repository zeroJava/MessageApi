using MessageApi.Domain;

namespace MessageApi.Application;

public interface ITokenValidator
{
   void Authenticate(AuthToken token);
   Task AuthenticateAsync(AuthToken token);
}

public class TokenValidator : ITokenValidator
{
   readonly IUserRepository userRepository;

   public TokenValidator(IUserRepository userRepository)
   {
      this.userRepository = userRepository;
   }

   public void Authenticate(AuthToken token)
   {
   }

   public async Task AuthenticateAsync(AuthToken token)
   {
      await Task.Run(() => { AuthenticateToken(token); }).ConfigureAwait(false);
   }

   void AuthenticateToken(AuthToken token)
   {
      // TODO Implement this
   }
}