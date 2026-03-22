
namespace MessageApi.Domain;

public sealed class InputValidationException : Exception
{
   public InputValidationException(string message) : base(message)
   {
   }
}

public class InputValidator : IInputValidator<string>
{
   public void Validate(string input)
   {
      ValidateInput(input);
   }

   public async Task ValidateAsync(string input)
   {
      await Task.Run(() => { ValidateInput(input); }).ConfigureAwait(false);
   }

   void ValidateInput(string input)
   {
      if (string.IsNullOrEmpty(input))
      {
         throw new InputValidationException("Input is empty");
      }
   }
}