using System.Threading.Tasks;

namespace UserAPI.Repository.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        Task SaveAsync();
    } 
}

