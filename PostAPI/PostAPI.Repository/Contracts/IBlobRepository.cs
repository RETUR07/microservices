
using PostAPI.Entities.Models;
using System.Threading.Tasks;

namespace PostAPI.Repository.Contracts
{
    public interface IBlobRepository : IRepositoryBase<Blob>
    {
        public Task<Blob> GetBlob(int blobId, bool trackChanges);
    }
}
