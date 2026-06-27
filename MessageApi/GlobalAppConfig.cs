using MessageApi.Application;
using MessageApi.Domain;
using MessageApi.Infrastructure;
using MessageApi.Infrastructure.CQRSImpl;
using MessageApi.Infrastructure.Sqlite;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MessageApi;

public static class GlobalAppConfig
{
   public static void InitialiseUserBuilder(WebApplicationBuilder builder)
   {
      builder.Services.AddScoped<IUserControllerBuilder>(b =>
      {
         IUserRepository userRepository = UserRepoFactory.GetRepository("sqlite");

         ICreateUserHandler createUserHandler = new CreateUserHandler(userRepository);
         IRetrieveUserHandler retrieveUserHandler = new RetrieveUserHandler(userRepository);
         UserFieldValidatorBase userFieldValidator = new UserFieldValidator(userRepository);

         UserControllerBuilder controllerBuilder = new();
         controllerBuilder.AddCreateUserHandler(createUserHandler).AddIRetrieveUserHandler(retrieveUserHandler)
            .AddUserFieldValidator(userFieldValidator).AddInputValidator(new InputValidator());
         return controllerBuilder;
      });
   }

   public static void InitialiseMessageBuilder(WebApplicationBuilder builder)
   {
      builder.Services.AddScoped<IMessageControllerBuilder>(b =>
      {
         IUserRepository userRepository = UserRepoFactory.GetRepository("sqlite");
         IMessageRepository messageRepository = MessageRepoFactory.GetRepository("sqlite");
         IMessageDispatchRepository dispatchRepository = MessageDispatcherRepoFactory.GetRepository("sqlite");
         IRepoTransaction repoTransaction = new RepoTransaction();

         ICreateMessageHandler createMessageHandler = new CreateMessageHandler(userRepository, messageRepository, dispatchRepository, repoTransaction);
         IGetConversationHandler getConversationHandler = new GetConversationHandler(userRepository, dispatchRepository);
         IGetMessagesToUserHandler getMessagesToUserHandler = new GetMessagesToUserHandler(userRepository, messageRepository, dispatchRepository);

         //IMessageServiceUseCase messageService = new MessageService(createMessageHandler, getConversationHandler, getMessagesToUserHandler);
         MessageControllerBuilder controllerBuilder = new();
         controllerBuilder.AddCreateMessageHandler(createMessageHandler).AddGetConversationHandler(getConversationHandler)
            .AddGetMessagesToUserHandler(getMessagesToUserHandler);
         return controllerBuilder;
      });
   }

   public static void InitialiseAuthenticationBuilder(WebApplicationBuilder builder)
   {
      builder.Services.AddScoped<IAuthenticationControllerBuilder>(b =>
      {
         IUserRepository userRepository = UserRepoFactory.GetRepository("sqlite");
         ITokenGenerator tokenGen = new SimpleJwtTokenGenerator();
         AuthenticationFieldValidatorBase authValidator = new AuthenticationFieldValidator();
         AuthenticationControllerBuilder controllerBuilder = new("sqlite");
         controllerBuilder.AddTokenGenerator(tokenGen).AddAuthenticationFieldValidator(authValidator).AddUserRepository(userRepository);
         return controllerBuilder;
      });
   }

   public static void InitialiseTokenGenerator(WebApplicationBuilder builder)
   {
      // Replace with proper security
      const string jwtKey = "helloworld123445789qwertyuiop123456789asdfghjkl123456789zxcvbnm123456789qazwsxedcrfvtgbyhnujmikolp123456789";
      const string issuer = "Message.Api";
      const string audience = "Testusers";
      builder.Services.AddAuthentication(options =>
      {
         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options =>
      {
         options.TokenValidationParameters = new TokenValidationParameters
         {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
         };
      });
      builder.Services.AddAuthorization();
   }
}