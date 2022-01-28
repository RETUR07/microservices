using MediatR;
using PostAPI.Application.Contracts;
using PostAPI.Application.CQRS.Queries;
using PostAPI.Application.DTO;
using PostAPI.Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PostAPI.Application.CQRS.Queries
{
    public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, PagedList<PostForResponseDTO>>
    {
        private readonly IPostService _postService;

        public GetPostsQueryHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<PagedList<PostForResponseDTO>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            return await _postService.GetPosts(request.UserId, request.Parameters);
        }
    }
}
