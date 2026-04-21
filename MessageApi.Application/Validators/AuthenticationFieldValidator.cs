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

   static void DoValidateAsync(AuthenticationRequest request, User? user)
   {
      if (user is null)
      {
         throw new AuthenticationValidationException("Error validationg user");
      }
      if (request.Username != user.UserName)
      {
         throw new AuthenticationValidationException("Username does not match");
      }
      if (request.Password != user.Password)
      {
         throw new AuthenticationValidationException("Password does not match");
      }
   }
}