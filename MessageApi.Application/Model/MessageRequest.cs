namespace MessageApi.Application;

public record MessageRequest
{
   public required string AccessToken { get; set; }
   public required string UserName { get; set; }
   public required string Message { get; set; }
   public required List<string> EmailAccounts { get; set; }
   public required DateTime MessageCreated { get; set; }
}