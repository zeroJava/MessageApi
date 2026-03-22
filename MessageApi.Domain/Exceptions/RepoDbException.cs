using System.Text;

namespace MessageApi.Domain;

public sealed class RepoDbException : Exception
{
   public DateTime EventTimestamp { get; init; }

   public RepoDbException(string message, Exception innerException) : base(message, innerException)
   {
      EventTimestamp = DateTime.Now;
   }

   public override string ToString()
   {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(Message + " on " + EventTimestamp);
      stringBuilder.AppendLine(base.ToString());

      return stringBuilder.ToString();
   }
}