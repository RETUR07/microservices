﻿using PostAPI.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostAPI.Repository.Contracts
{
    public interface IRateRepository : IRepositoryBase<Rate>
    {
        Task<List<Rate>> GetRatesByPostIdAsync(int postId, bool trackChanges);
        Task<Rate> GetPostRateAsync(string userId, int postId, bool trackChanges);
    }
}
