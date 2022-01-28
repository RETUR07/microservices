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
    public class GetChildPostsQueryHandler : IRequestHandler<GetChildPostsQuery, PagedList<PostForResponseDTO>>
    {
        private readonly IPostService _postService;

        public GetChildPostsQueryHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<PagedList<PostForResponseDTO>> Handle(GetChildPostsQuery request, CancellationToken cancellationToken)
        {
            return await _postService.GetChildPosts(request.PostId, request.Parameters);
        }
    }
}
