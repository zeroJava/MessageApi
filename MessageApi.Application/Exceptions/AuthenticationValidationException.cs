namespace MessageApi.Application;

public class AuthenticationValidationException : Exception
{
   public AuthenticationValidationException(string message) : base(message)
   {
      //
   }
}