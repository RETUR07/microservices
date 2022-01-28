using ChatAPI.Entities.Models;
using System.Threading.Tasks;

namespace ChatAPI.Repository.Contracts
{
    public interface IBlobRepository : IRepositoryBase<Blob>
    {
        public Task<Blob> GetBlob(int blobId, bool trackChanges);
    }
}
