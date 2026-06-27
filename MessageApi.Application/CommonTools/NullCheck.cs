using System.Diagnostics.CodeAnalysis;

namespace MessageApi.Application;

public class NullCheck
{
   public static T GetResultOrError<T>([NotNullIfNotNull(nameof(result))] T? result)
   {
      ApplicationException error(string name)
      {
         return new ApplicationException($"Property: {name} was not initialised");
      }
      return result ?? throw error(nameof(result));
   }

   public static void ErrorIfNull<T>([NotNull] T? result)
   {
      if (result is null)
      {
         throw new ApplicationException($"Property: {nameof(result)} was not initialised");
      }
   }
}