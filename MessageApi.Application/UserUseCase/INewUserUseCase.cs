namespace MessageApi.Application;

public interface INewUserUseCase
{
   Task<UserDto> Create(NewUserData newuser);
}