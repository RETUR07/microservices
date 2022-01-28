using ChatAPI.Entities.Models;
using ChatAPI.Entities.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatAPI.Repository.Contracts
{
    public interface IChatRepository : IRepositoryBase<Chat>
    {
        public Task<List<Chat>> GetChatsAsync(string userId, bool trackChanges);
        public Task<Chat> GetChatAsync(int chatId, bool trackChanges);
    }
}
