namespace MessageApi.Application;

public interface IRetrieveUserHandler
{
   Task<UserDto?> Handle(string username);
}