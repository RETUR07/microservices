using AutoMapper;
using PostAPI.Application.Contracts;
using PostAPI.Application.DTO;
using PostAPI.Entities.Models;
using PostAPI.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostAPI.Application.Services
{
    public class RateService : IRateService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public RateService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PostRateForResponseDTO> GetPostRateAsync(string userId, int postId)
        {
            var rate = await _repository.Rate.GetPostRateAsync(userId, postId, false);
            return _mapper.Map<Rate, PostRateForResponseDTO>(rate);
        }

        public async Task<List<PostRateForResponseDTO>> GetRatesByPostIdAsync(int postId)
        {
            var rates = await _repository.Rate.GetRatesByPostIdAsync(postId, false);
            return _mapper.Map<List<Rate>, List<PostRateForResponseDTO>>(rates);
        }

        public async Task<List<List<PostRateForResponseDTO>>> GetRatesByPostsIdsAsync(List<int> postIds)
        {
            List<List<PostRateForResponseDTO>> PostsRates = new List<List<PostRateForResponseDTO>>();
            foreach (var postId in postIds)
            {
                var rates = await _repository.Rate.GetRatesByPostIdAsync(postId, false);
                var responseRates = _mapper.Map<List<Rate>, List<PostRateForResponseDTO>>(rates);
                PostsRates.Add(responseRates);
            }
            
            return PostsRates;
        }

        public async Task UpdatePostRateAsync(RateForm ratedto, string userId)
        {
            if (ratedto == null) return;
            var rate = await _repository.Rate.GetPostRateAsync(userId, ratedto.ObjectId, true);
            if (rate == null)
            {
                var post = await _repository.Post.GetPostAsync(ratedto.ObjectId, true);
                if (post == null) return;
                rate = _mapper.Map<RateForm, Rate>(ratedto);
                rate.User.UserId = userId;
                _repository.Rate.Create(rate);
            }
            else
            {
                if (ratedto.LikeStatus == rate.LikeStatus)
                {
                    rate.LikeStatus = LikeStatus.Viewed;
                }
                else
                   rate.LikeStatus = ratedto.LikeStatus;               
            }
            await _repository.SaveAsync();
            return;
        }
    }
}
