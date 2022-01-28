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
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IPostService _postService;
        private readonly IPostDeletedSender _postDeletedSender;

        public DeletePostCommandHandler(IPostService postService, IPostDeletedSender postDeletedSender)
        {
            _postService = postService;
            _postDeletedSender = postDeletedSender;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            await _postService.DeletePost(request.PostId, request.UserId);
            _postDeletedSender.Send(new PostForSender() { UserId = request.UserId, PostId = request.PostId });
            return Unit.Value;
        }
    }
}
