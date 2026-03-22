namespace MessageApi.Application;

public record UserRequest
{
   public required AuthToken AuthToken { get; set; }
   public required string UserName { get; set; }
}