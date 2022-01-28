using PostAPI.Application.DTO;
using PostAPI.Entities.Models;
using PostAPI.Entities.RequestFeatures;
using System.Threading.Tasks;

namespace PostAPI.Application.Contracts
{
    public interface IPostService
    {
        public Task<Post> CreatePost(PostForm postdto, string userId);
        public Task DeletePost(int postId, string userId);
        public Task<PostForResponseDTO> GetPost(int postId);
        public Task<PagedList<PostForResponseDTO>> GetPosts(string userId, Parameters parameters);
        public Task<PagedList<PostForResponseDTO>> GetChildPosts(int postId, Parameters parameters);
    }
}
