using MessageApi.Application;
using MessageApi.Domain;
using MessageApi.Infrastructure.Sqlite;

namespace MessageApi;

public class MessageControllerCreatorBuilder : IMessageControllerCreatorBuilder
{
   IUserRepository? userRepository;
   IMessageRepository? messageRepository;
   IMessageDispatchRepository? messageDispatchRepository;
   IRepoTransaction? repoTransaction;
   IMessageCreator? messageCreator;
   IMessageCreatorUseCase? messageCreatorUseCase;
   ITokenValidator? tokenValidator;

   public IMessageControllerCreatorBuilder AddMessageCreator(IMessageCreator creator)
   {
      messageCreator = creator;
      return this;
   }

   public IMessageControllerCreatorBuilder AddMessageDispatchRepository(IMessageDispatchRepository repository)
   {
      messageDispatchRepository = repository;
      return this;
   }

   public IMessageControllerCreatorBuilder AddMessageRepository(IMessageRepository repository)
   {
      messageRepository = repository;
      return this;
   }

   public IMessageControllerCreatorBuilder AddNewMessageCreatorUseCase(IMessageCreatorUseCase creatorUseCase)
   {
      messageCreatorUseCase = creatorUseCase;
      return this;
   }

   public IMessageControllerCreatorBuilder AddRepoTransaction(IRepoTransaction repoTransaction)
   {
      this.repoTransaction = repoTransaction;
      return this;
   }

   public IMessageControllerCreatorBuilder AddTokenValidator(ITokenValidator tokenValidator)
   {
      this.tokenValidator = tokenValidator;
      return this;
   }

   public IMessageControllerCreatorBuilder AddUserRepository(IUserRepository repository)
   {
      userRepository = repository;
      return this;
   }

   public MessageControllerCreatorOption Build()
   {
      IUserRepository userRepo = userRepository ?? UserRepoFactory.GetRepository("sqlite");
      IMessageRepository messageRepo = messageRepository ?? MessageRepoFactory.GetRepository("sqlite");
      IMessageDispatchRepository dispatchRepo = messageDispatchRepository ?? MessageDispatcherRepoFactory.GetRepository("sqlite");
      IRepoTransaction repoTransaction = this.repoTransaction ?? new RepoTransaction();
      IMessageCreator creator = messageCreator ?? new MessageCreator(userRepo, messageRepo, dispatchRepo, repoTransaction);
      ITokenValidator tokenValidator = this.tokenValidator ?? new TokenValidator(userRepo);
      IMessageCreatorUseCase messageCreatorService = messageCreatorUseCase ?? new NewMessageService(creator, tokenValidator);
      return new MessageControllerCreatorOption()
      {
         MessageCreator = creator,
         MessageDispatchRepository = dispatchRepo,
         MessageRepository = messageRepo,
         NewMessageService = messageCreatorService,
         RepoTransaction = repoTransaction,
         TokenValidator = tokenValidator,
         UserRepository = userRepo,
      };
   }
}