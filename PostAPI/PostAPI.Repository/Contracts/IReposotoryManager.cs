using System.Threading.Tasks;

namespace PostAPI.Repository.Contracts
{
    public interface IRepositoryManager
    {
        IPostRepository Post { get; }
        IBlobRepository Blob { get; }
        IRateRepository Rate { get; }
        IUserRepository User { get; }
        Task SaveAsync();
    } 
}

