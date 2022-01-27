using System.Collections.Generic;
using System.Threading.Tasks;
using UserAPI.Entities.Models;

namespace UserAPI.Repository.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<List<User>> GetAllUsersAsync(bool trackChanges);
        Task<User> GetUserAsync(string userId, bool trackChanges);
    }
}
