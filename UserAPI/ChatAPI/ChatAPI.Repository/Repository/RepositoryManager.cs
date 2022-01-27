using ChatAPI.Repository.Contracts;
using System.Threading.Tasks;

namespace ChatAPI.Repository.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;

        private IBlobRepository _blobRepository;
        private IChatRepository _chatRepository;
        private IMessageRepository _messageRepository;
        private IUserRepository _userRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IBlobRepository Blob
        {
            get
            {
                if (_blobRepository == null)
                    _blobRepository = new BlobRepository(_repositoryContext);
                return _blobRepository;
            }
        }

        public IChatRepository Chat
        {
            get
            {
                if (_chatRepository == null)
                    _chatRepository = new ChatRepository(_repositoryContext);
                return _chatRepository;
            }
        }

        public IMessageRepository Message
        {
            get
            {
                if (_messageRepository == null)
                    _messageRepository = new MessageRepository(_repositoryContext);
                return _messageRepository;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_repositoryContext);
                return _userRepository;
            }
        }
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
