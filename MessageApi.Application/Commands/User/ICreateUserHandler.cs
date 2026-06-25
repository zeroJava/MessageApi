namespace MessageApi.Application;

public interface ICreateUserHandler
{
   Task<NewUserResponse> Handle(NewUserData newuser);
}

public record NewUserResponse
{
   public long Id { get; set; }
   public required string UserName { get; set; }
   public bool State { get; set; }
}