namespace MessageApi.Application;

public interface IRetrieveUserUseCase
{
   Task<UserDto?> GetUser(AuthToken token, string username);
}