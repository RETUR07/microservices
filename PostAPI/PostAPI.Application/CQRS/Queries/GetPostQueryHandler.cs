using MediatR;
using PostAPI.Application.Contracts;
using PostAPI.Application.CQRS.Queries;
using PostAPI.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PostAPI.Application.CQRS.Queries
{
    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostForResponseDTO>
    {
        private readonly IPostService _postService;

        public GetPostQueryHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<PostForResponseDTO> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            return await _postService.GetPost(request.PostId);
        }
    }
}
