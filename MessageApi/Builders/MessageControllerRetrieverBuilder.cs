using MessageApi.Application;
using MessageApi.Domain;

namespace MessageApi;

public class MessageControllerRetrieverBuilder : IMessageControllerRetrieverBuilder
{
   IUserRepository? userRepository;
   IMessageRepository? messageRepository;
   IMessageDispatchRepository? messageDispatchRepository;
   IMessageRetriever? messageRetriever;
   IMessageRetrieverUseCase? messageRetrieverUseCase;
   ITokenValidator? tokenValidator;

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

   public IMessageControllerRetrieverBuilder AddTokenValidator(ITokenValidator tokenValidator)
   {
      this.tokenValidator = tokenValidator;
      return this;
   }

   public IMessageControllerRetrieverBuilder AddUserRepository(IUserRepository repository)
   {
      userRepository = repository;
      return this;
   }

   public MessageControllerRetrieverOption Build()
   {
      IUserRepository userRepo = userRepository ?? UserRepoFactory.GetRepository("sqlite");
      IMessageRepository messageRepo = messageRepository ?? MessageRepoFactory.GetRepository("sqlite");
      IMessageDispatchRepository dispatchRepo = messageDispatchRepository ?? MessageDispatcherRepoFactory.GetRepository("sqlite");
      IMessageRetriever retriever = messageRetriever ?? new MessageRetriever(userRepo, messageRepo, dispatchRepo);
      ITokenValidator tokenValidator = this.tokenValidator ?? new TokenValidator(userRepo);
      IMessageRetrieverUseCase messageRetrieveService = messageRetrieverUseCase ?? new RetrieveMessageService(retriever, tokenValidator);
      return new MessageControllerRetrieverOption()
      {
         MessageRetriever = retriever,
         MessageDispatchRepository = dispatchRepo,
         MessageRepository = messageRepo,
         RetrieveMessageService = messageRetrieveService,
         TokenValidator = tokenValidator,
         UserRepository = userRepo,
      };
   }
}