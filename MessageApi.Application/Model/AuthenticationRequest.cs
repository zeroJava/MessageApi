namespace MessageApi.Application;

public record AuthenticationRequest
{
   public required string Username { get; set; }
   public required string Password { get; set; }
}