using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi;

public interface IMessageControllerRetrieverBuilder
{
   IMessageControllerRetrieverBuilder AddUserRepository(IUserRepository repository);
   IMessageControllerRetrieverBuilder AddMessageRepository(IMessageRepository repository);
   IMessageControllerRetrieverBuilder AddMessageDispatchRepository(IMessageDispatchRepository repository);
   IMessageControllerRetrieverBuilder AddMessageRetriever(IMessageRetriever retriever);
   IMessageControllerRetrieverBuilder AddNewMessageRetrieverUseCase(IMessageRetrieverUseCase retrieverUseCase);
   MessageControllerRetrieverOption Build();
}

public sealed class MessageControllerRetrieverOption
{
   public required IUserRepository UserRepository { get; set; }
   public required IMessageRepository MessageRepository { get; set; }
   public required IMessageDispatchRepository MessageDispatchRepository { get; set; }
   public required IMessageRetriever MessageRetriever { get; set; }
   public required IMessageRetrieverUseCase RetrieveMessageService { get; set; }
}