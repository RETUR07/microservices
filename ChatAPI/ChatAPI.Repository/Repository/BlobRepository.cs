using Microsoft.EntityFrameworkCore;
using ChatAPI.Entities.Models;
using ChatAPI.Repository.Contracts;
using System.Threading.Tasks;

namespace ChatAPI.Repository.Repository
{
    public class BlobRepository : RepositoryBase<Blob>, IBlobRepository
    {
        public BlobRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<Blob> GetBlob(int blobId, bool trackChanges) =>
             await FindByCondition(b => b.Id == blobId, trackChanges).SingleOrDefaultAsync();

    }
}
