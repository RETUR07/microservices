using Microsoft.EntityFrameworkCore;
using ChatAPI.Entities.Models;
using ChatAPI.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ChatAPI.Repository.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {

        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public async Task<User> GetUserAsync(string userId, bool trackChanges) =>
            await FindByCondition(u => u.UserId == userId, trackChanges)
            .SingleOrDefaultAsync();
    }
}
