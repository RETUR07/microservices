using PostAPI.Repository.Contracts;
using System.Threading.Tasks;

namespace PostAPI.Repository.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;

        private IPostRepository _postRepository;
        private IBlobRepository _blobRepository;
        private IRateRepository _rateRepository;
        private IUserRepository _userRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
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

        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_repositoryContext);
                return _userRepository;
            }
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

        public IRateRepository Rate
        {
            get
            {
                if (_rateRepository == null)
                    _rateRepository = new RateRepository(_repositoryContext);
                return _rateRepository;
            }
        }

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
