using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi;

public interface IMessageControllerCreatorBuilder
{
   IMessageControllerCreatorBuilder AddUserRepository(IUserRepository repository);
   IMessageControllerCreatorBuilder AddMessageRepository(IMessageRepository repository);
   IMessageControllerCreatorBuilder AddMessageDispatchRepository(IMessageDispatchRepository repository);
   IMessageControllerCreatorBuilder AddRepoTransaction(IRepoTransaction repoTransaction);
   IMessageControllerCreatorBuilder AddMessageCreator(IMessageCreator creator);
   IMessageControllerCreatorBuilder AddNewMessageCreatorUseCase(IMessageCreatorUseCase creatorUseCase);
   IMessageControllerCreatorBuilder AddTokenValidator(ITokenValidator tokenValidator);
   MessageControllerCreatorOption Build();
}

public sealed class MessageControllerCreatorOption
{
   public required IUserRepository UserRepository { get; set; }
   public required IMessageRepository MessageRepository { get; set; }
   public required IMessageDispatchRepository MessageDispatchRepository { get; set; }
   public required IRepoTransaction RepoTransaction { get; set; }
   public required IMessageCreator MessageCreator { get; set; }
   public required IMessageCreatorUseCase NewMessageService { get; set; }
   public required ITokenValidator TokenValidator { get; set; }
}