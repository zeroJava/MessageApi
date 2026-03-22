namespace MessageApi.Application;

public class NewMessageService : IMessageCreatorUseCase
{
   readonly IMessageCreator messageCreator;
   readonly ITokenValidator tokenValidator;
   readonly Helper helper = new();

   public NewMessageService(IMessageCreator messageCreator, ITokenValidator tokenValidator)
   {
      this.messageCreator = messageCreator;
      this.tokenValidator = tokenValidator;
   }

   public async Task<MessageRequestState> Create(MessageRequest request)
   {
      AuthToken authToken = helper.GetAuthToken(request);
      await tokenValidator.AuthenticateAsync(authToken).ConfigureAwait(false);
      return messageCreator.Create(request);
   }

   class Helper
   {
      public AuthToken GetAuthToken(MessageRequest request)
      {
         return new AuthToken
         {
            UserName = request.UserName,
            Token = request.AccessToken,
         };
      }
   }
}