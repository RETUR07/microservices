using UserAPI.Repository.Contracts;
using System.Threading.Tasks;

namespace UserAPI.Repository.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;

        private IUserRepository _userRepository;
        private IPostRepository _postRepository;
        private IChatRepository _chatRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
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

        public IPostRepository Post
        {
            get
            {
                if (_postRepository == null)
                    _postRepository = new PostRepository(_repositoryContext);
                return _postRepository;
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

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
