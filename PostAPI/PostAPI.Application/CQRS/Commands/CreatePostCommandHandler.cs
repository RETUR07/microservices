using PostAPI.Application.Contracts;
using PostAPI.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PostAPI.Messaging.Send.Sender;
using PostAPI.Messaging.Send.DTO;

namespace PostAPI.Application.CQRS.Commands
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand>
    {
        private readonly IPostService _postService;
        private readonly IPostCreatedSender _postCreatedSender;

        public CreatePostCommandHandler(IPostService postService, IPostCreatedSender postCreatedSender)
        {
            _postService = postService;
            _postCreatedSender = postCreatedSender;
        }

        public async Task<Unit> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postService.CreatePost(request.PostDTO, request.UserId);
            if(post!=null)_postCreatedSender.Send(new PostForSender() { UserId = request.UserId, PostId = post.Id });
            return Unit.Value;
        }
    }
}
