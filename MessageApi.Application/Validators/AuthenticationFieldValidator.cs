using MessageApi.Domain;

namespace MessageApi.Application;

public abstract class AuthenticationFieldValidatorBase
{
   public abstract Task ValidateFieldAsync(AuthenticationRequest input);
   public abstract Task ValidateAsync(AuthenticationRequest input, User? user);
}

public class AuthenticationFieldValidator : AuthenticationFieldValidatorBase
{
   public override async Task ValidateFieldAsync(AuthenticationRequest request)
   {
      await Task.Run(() =>
      {
         DoValidateField(request);
      }).ConfigureAwait(false);
   }

   static void DoValidateField(AuthenticationRequest request)
   {
      if (string.IsNullOrEmpty(request?.Username))
      {
         throw new AuthenticationValidationException("Username is empty");
      }
      if (string.IsNullOrEmpty(request?.Password))
      {
         throw new AuthenticationValidationException("Password is empty");
      }
   }

   public override async Task ValidateAsync(AuthenticationRequest request, User? user)
   {
      await Task.Run(() =>
      {
         DoValidateAsync(request, user);
      }).ConfigureAwait(false);
   }

   void DoValidateAsync(AuthenticationRequest request, User? user)
   {
      if (user is null)
      {
         throw new AuthenticationValidationException("Error validationg user");
      }
      CheckUsernameMatch(request, user);
      CheckPasswordMatch(request, user);
   }

   void CheckUsernameMatch(AuthenticationRequest request, User user)
   {
      string decryptedRequestUsername = request.Username;
      string decryptedUserUsername = user.UserName;
      if (decryptedRequestUsername != decryptedUserUsername)
      {
         throw new AuthenticationValidationException("Username does not match");
      }
   }

   void CheckPasswordMatch(AuthenticationRequest request, User user)
   {
      string decryptedRequestPassword = request.Password;
      string decryptedUserPassword = user.Password;
      if (decryptedRequestPassword != decryptedUserPassword)
      {
         throw new AuthenticationValidationException("Password does not match");
      }
   }
}