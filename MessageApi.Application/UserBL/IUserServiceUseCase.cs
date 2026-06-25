namespace MessageApi.Application;

public interface IUserServiceUseCase
{
   Task<NewUserResponse> Create(NewUserData newuser);
   Task<UserDto?> GetUser(AuthToken token, string username);
}