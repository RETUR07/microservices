using Microsoft.EntityFrameworkCore;
using ChatAPI.Entities.Models;
using ChatAPI.Entities.RequestFeatures;
using ChatAPI.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Repository.Repository
{
    public class ChatRepository : RepositoryBase<Chat>, IChatRepository
    {
        public ChatRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<List<Chat>> GetChatsAsync(string userId, bool trackChanges)
            => await FindByCondition(ch => ch.UserIds.Select(x => x.UserId).Contains(userId), trackChanges)
            .Include(x => x.UserIds)
            .ToListAsync();

        public async Task<Chat> GetChatAsync(int chatId, bool trackChanges)
            => await FindByCondition(ch => ch.Id == chatId, trackChanges)
            .Include(x => x.UserIds)
            .Include(x => x.Messages.Where(x => x.IsEnable)).ThenInclude(x => x.UserId)
            .Include(x => x.Messages.Where(x => x.IsEnable)).ThenInclude(x => x.Blobs)
            .SingleOrDefaultAsync();
    }
}
