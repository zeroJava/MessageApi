namespace MessageApi.Application;

public record AuthToken
{
   public required string UserName { get; set; }
   public required string Token { get; set; }
}