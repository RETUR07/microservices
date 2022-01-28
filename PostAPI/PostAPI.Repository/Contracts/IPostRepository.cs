﻿using PostAPI.Entities.Models;
using PostAPI.Entities.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostAPI.Repository.Contracts
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        public PagedList<Post> GetAllPostsPaged(string userId, Parameters parameters, bool trackChanges);
        public Task<List<Post>> GetChildrenPostsByPostIdAsync(int postId, bool trackChanges);
        public PagedList<Post> GetChildrenPostsByPostIdPaged(int postId, Parameters parameters, bool trackChanges);
        public Task<Post> GetPostAsync(int postId, bool trackChanges);
    }
}
