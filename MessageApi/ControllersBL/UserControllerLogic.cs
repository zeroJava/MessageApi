using MessageApi.Application;

namespace MessageApi.Controllers;

public static class UserControllerLogic
{
   public static async Task<NewUserResponse> CreateUser(NewUserData newuser, IUserControllerBuilder controllerBuilder)
   {
      UserControllerOption option = controllerBuilder.Build();
      IUserServiceUseCase userService = option.UserService;
      return await userService.Create(newuser);
   }

   public static async Task<UserDto> GetUser(UserRequest userRequest, IUserControllerBuilder controllerBuilder)
   {
      UserControllerOption option = controllerBuilder.Build();
      IUserServiceUseCase userService = option.UserService;
      return await userService.GetUser(userRequest.AuthToken, userRequest.UserName).ConfigureAwait(false);
   }
}