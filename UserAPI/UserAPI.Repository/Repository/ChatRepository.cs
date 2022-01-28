using Microsoft.EntityFrameworkCore;
using UserAPI.Entities.Models;
using UserAPI.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Repository.Repository
{
    public class ChatRepository : RepositoryBase<Chat>, IChatRepository
    {
        public ChatRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
    }
}
