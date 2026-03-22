using MessageApi.Application;

namespace MessageApi.Controllers;

public static class UserControllerLogic
{
   public static async Task<UserDto> CreateUser(NewUserData newuser, IUserControllerCreatorBuilder controllerCreatorBuilder)
   {
      UserControllerCreatorOption option = controllerCreatorBuilder.Build();
      INewUserUseCase newUserService = option.NewUserService;
      return await newUserService.Create(newuser);
   }

   public static async Task<UserDto> GetUser(UserRequest userRequest, IUserControllerRerieverBuilder controllerLogicBuilder)
   {
      UserControllerRetrieverOption option = controllerLogicBuilder.Build();
      IRetrieveUserUseCase retrieveUser = option.RetrieveUser;
      UserDto? userDto = await retrieveUser.GetUser(userRequest.AuthToken, userRequest.UserName).ConfigureAwait(false);
      return userDto is null ?
         throw new ApplicationException($"Could not find user matching {userRequest.UserName}") : userDto;
   }
}