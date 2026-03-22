namespace MessageApi.Application;

public record UserDto
{
   public long Id { get; set; }
   public required string UserName { get; set; }
}