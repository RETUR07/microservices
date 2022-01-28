using Microsoft.EntityFrameworkCore;
using PostAPI.Entities.Models;
using PostAPI.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostAPI.Repository.Repository
{
    public class RateRepository : RepositoryBase<Rate>, IRateRepository
    {
        public RateRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<Rate> GetPostRateAsync(string userId, int postId, bool trackChanges) =>
           await FindByCondition(r => r.User.UserId == userId && r.PostId == postId, trackChanges)
            .SingleOrDefaultAsync();

        public async Task<List<Rate>> GetRatesByPostIdAsync(int postId, bool trackChanges) =>
           await FindByCondition(r => r.PostId == postId, trackChanges)
            .ToListAsync();
    }
}
