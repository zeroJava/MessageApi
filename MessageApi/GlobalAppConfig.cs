using MessageApi.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MessageApi;

public static class GlobalAppConfig
{
   public static void InitialiseUserBuilder(WebApplicationBuilder builder)
   {
      builder.Services.AddScoped<IUserControllerRerieverBuilder>(b =>
      {
         IUserRepository userRepository = UserRepoFactory.GetRepository("sqlite");
         UserControllerRetrieverBuilder controllerBuilder = new();
         controllerBuilder.AddUserRepository(userRepository).AddInputValidator(new InputValidator());
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
         controllerBuilder.AddMessageRepository(messageRepository).AddUserRepository(userRepository);
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