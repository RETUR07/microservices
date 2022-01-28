using Microsoft.EntityFrameworkCore;
using UserAPI.Entities.Models;
using UserAPI.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserAPI.Repository.Repository
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
    }
}
