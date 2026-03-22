namespace MessageApi.Domain;

public interface IInputValidator<in T> where T : class
{
   void Validate(T input);
   Task ValidateAsync(T input);
}