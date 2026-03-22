namespace MessageApi.Application;

public class RetrieveMessageService : IMessageRetrieverUseCase
{
   readonly IMessageRetriever messageRetriever;
   readonly ITokenValidator tokenValidator;
   readonly Helper helper = new();

   public RetrieveMessageService(IMessageRetriever messageRetriever, ITokenValidator tokenValidator)
   {
      this.messageRetriever = messageRetriever;
      this.tokenValidator = tokenValidator;
   }

   public async Task<List<MessageInfo>> GetConversation(RetrieveMessageRequest messageRequest)
   {
      AuthToken authToken = helper.GetAuthToken(messageRequest);
      await tokenValidator.AuthenticateAsync(authToken).ConfigureAwait(false);
      return messageRetriever.GetConversation(messageRequest);
   }

   public async Task<List<MessageInfo>> GetMessagesSentToUser(RetrieveMessageRequest messageRequest)
   {
      AuthToken authToken = helper.GetAuthToken(messageRequest);
      await tokenValidator.AuthenticateAsync(authToken).ConfigureAwait(false);
      return messageRetriever.GetMessagesSentToUser(messageRequest);
   }

   class Helper
   {
      public AuthToken GetAuthToken(RetrieveMessageRequest request)
      {
         return new AuthToken
         {
            UserName = request.Username,
            Token = request.UserAccessToken,
         };
      }
   }
}