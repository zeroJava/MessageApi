namespace MessageApi.Domain;

public sealed class SchemaException : Exception
{
   public SchemaException(string message) : base(message)
   {
      //
   }
}