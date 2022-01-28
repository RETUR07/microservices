using ChatAPI.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatAPI.Application.CQRS.Commands
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand>
    {
        private readonly IChatService _chatService;

        public DeleteMessageCommandHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<Unit> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            await _chatService.DeleteMessage(request.UserId, request.MessageId);
            return Unit.Value;
        }
    }
}
