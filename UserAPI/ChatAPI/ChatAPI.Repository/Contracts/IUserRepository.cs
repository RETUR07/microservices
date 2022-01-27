using ChatAPI.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatAPI.Repository.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetUserAsync(string userId, bool trackChanges);
    }
}
