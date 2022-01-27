using Microsoft.AspNetCore.Identity;
using UserAPI.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserAPI.Application.Contracts
{
    public interface IUserService
    {
        public Task<List<UserForResponseDTO>> GetUsersAsync();
        public Task<UserForResponseDTO> GetUserAsync(string userId);
        public Task<IdentityResult> CreateUserAsync(UserRegistrationForm userdto);
        public Task UpdateUserAsync(string userId, UserForm userdto);
        public Task DeleteUserAsync(string userId);
        public Task AddFriendAsync(string userId, string friendId);
        public Task DeleteFriendAsync(string userId, string friendId);
    }
}
