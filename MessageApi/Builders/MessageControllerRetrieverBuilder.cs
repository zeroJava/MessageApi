using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi;

public class MessageControllerRetrieverBuilder : IMessageControllerRetrieverBuilder
{
   readonly string defaultDbOption;

   IUserRepository? userRepository;
   IMessageRepository? messageRepository;
   IMessageDispatchRepository? messageDispatchRepository;
   IMessageRetriever? messageRetriever;
   IMessageRetrieverUseCase? messageRetrieverUseCase;

   public MessageControllerRetrieverBuilder(string defaultDbOption)
   {
      this.defaultDbOption = defaultDbOption;
   }

   public IMessageControllerRetrieverBuilder AddMessageDispatchRepository(IMessageDispatchRepository repository)
   {
      messageDispatchRepository = repository;
      return this;
   }

   public IMessageControllerRetrieverBuilder AddMessageRepository(IMessageRepository repository)
   {
      messageRepository = repository;
      return this;
   }

   public IMessageControllerRetrieverBuilder AddMessageRetriever(IMessageRetriever retriever)
   {
      messageRetriever = retriever;
      return this;
   }

   public IMessageControllerRetrieverBuilder AddNewMessageRetrieverUseCase(IMessageRetrieverUseCase retrieverUseCase)
   {
      messageRetrieverUseCase = retrieverUseCase;
      return this;
   }

   public IMessageControllerRetrieverBuilder AddUserRepository(IUserRepository repository)
   {
      userRepository = repository;
      return this;
   }

   public MessageControllerRetrieverOption Build()
   {
      IUserRepository userRepo = userRepository ?? UserRepoFactory.GetRepository(defaultDbOption);
      IMessageRepository messageRepo = messageRepository ?? MessageRepoFactory.GetRepository(defaultDbOption);
      IMessageDispatchRepository dispatchRepo = messageDispatchRepository ?? MessageDispatcherRepoFactory.GetRepository(defaultDbOption);
      IMessageRetriever retriever = messageRetriever ?? new MessageRetriever(userRepo, messageRepo, dispatchRepo);
      IMessageRetrieverUseCase messageRetrieveService = messageRetrieverUseCase ?? new RetrieveMessageService(retriever);
      return new MessageControllerRetrieverOption()
      {
         MessageRetriever = retriever,
         MessageDispatchRepository = dispatchRepo,
         MessageRepository = messageRepo,
         RetrieveMessageService = messageRetrieveService,
         UserRepository = userRepo,
      };
   }
}