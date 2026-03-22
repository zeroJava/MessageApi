namespace MessageApi.Application;

public interface IMessageCreatorUseCase
{
   Task<MessageRequestState> Create(MessageRequest request);
}
