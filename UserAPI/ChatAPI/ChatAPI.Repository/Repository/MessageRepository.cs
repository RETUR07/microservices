﻿using Microsoft.EntityFrameworkCore;
using ChatAPI.Entities.Models;
using ChatAPI.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Repository.Repository
{
    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        public MessageRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<Message> GetMessageAsync(int messageId, bool trackChanges)
            => await FindByCondition(m => m.Id == messageId, trackChanges)
            .Include(x => x.Chat)
            .Include(x => x.Blobs)
            .Include(x => x.UserId)
            .SingleOrDefaultAsync();

        public async Task<List<Message>> GetMessgesByChatIdAsync(int chatId, bool trackChanges)
            => await FindByCondition(m => m.Chat.Id == chatId, trackChanges).ToListAsync();
    }
}
