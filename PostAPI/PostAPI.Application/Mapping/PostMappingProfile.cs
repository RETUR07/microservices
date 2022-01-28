using AutoMapper;
using PostAPI.Application.DTO;
using PostAPI.Application.Services;
using PostAPI.Entities.Models;

namespace PostAPI.Application.Mapping
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<PostForm, Post>();

            CreateMap<Post, PostForResponseDTO>()
                .ForMember(p => p.UserId, opt => opt.MapFrom(x => x.Author.Id))
                .ForMember(p => p.ParentPostId, opt => opt.MapFrom(x => x.ParentPost.Id));
        }
    }
}
