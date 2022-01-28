using PostAPI.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostAPI.Application.Contracts
{
    public interface IRateService
    {
        public Task<PostRateForResponseDTO> GetPostRateAsync(string userId, int postId);
        public Task<List<PostRateForResponseDTO>> GetRatesByPostIdAsync(int postId);
        public Task<List<List<PostRateForResponseDTO>>> GetRatesByPostsIdsAsync(List<int> postIds);
        public Task UpdatePostRateAsync(RateForm rate, string userId);
    }
}
