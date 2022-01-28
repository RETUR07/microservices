using System.Threading.Tasks;

namespace UserAPI.Repository.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        IPostRepository Post { get; }
        IChatRepository Chat { get; }
        Task SaveAsync();
    } 
}

