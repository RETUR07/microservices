using System.Threading.Tasks;

namespace ChatAPI.Repository.Contracts
{
    public interface IRepositoryManager
    {
        IBlobRepository Blob { get; }
        IMessageRepository Message { get; }
        IChatRepository Chat { get; }
        IUserRepository User { get; }
        Task SaveAsync();
    } 
}

