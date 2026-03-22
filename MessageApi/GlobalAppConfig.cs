using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi;

public static class GlobalAppConfig
{
   public static void InitialiseUserBuilder(WebApplicationBuilder builder)
   {
      builder.Services.AddScoped<IUserControllerRerieverBuilder>(b =>
      {
         IUserRepository userRepository = UserRepoFactory.GetRepository("sqlite");
         UserControllerRetrieverBuilder controllerBuilder = new();
         controllerBuilder.AddUserRepository(userRepository).AddInputValidator(new InputValidator()).AddTokenValidator(new TokenValidator(userRepository));
         return controllerBuilder;
      });

      builder.Services.AddScoped<IUserControllerCreatorBuilder>(b =>
      {
         IUserRepository userRepository = UserRepoFactory.GetRepository("sqlite");
         UserControllerCreatorBuilder controllerBuilder = new();
         controllerBuilder.AddUserRepository(userRepository);
         return controllerBuilder;
      });
   }

   public static void InitialiseMessageBuilder(WebApplicationBuilder builder)
   {
      builder.Services.AddScoped<IMessageControllerRetrieverBuilder>(b =>
      {
         IUserRepository userRepository = UserRepoFactory.GetRepository("sqlite");
         IMessageRepository messageRepository = MessageRepoFactory.GetRepository("sqlite");
         MessageControllerRetrieverBuilder controllerBuilder = new();
         controllerBuilder.AddMessageRepository(messageRepository).AddUserRepository(userRepository).AddTokenValidator(new TokenValidator(userRepository));
         return controllerBuilder;
      });

      builder.Services.AddScoped<IMessageControllerCreatorBuilder>(b =>
      {
         IUserRepository userRepository = UserRepoFactory.GetRepository("sqlite");
         IMessageRepository messageRepository = MessageRepoFactory.GetRepository("sqlite");
         MessageControllerCreatorBuilder controllerBuilder = new();
         controllerBuilder.AddMessageRepository(messageRepository).AddUserRepository(userRepository);
         return controllerBuilder;
      });
   }
}