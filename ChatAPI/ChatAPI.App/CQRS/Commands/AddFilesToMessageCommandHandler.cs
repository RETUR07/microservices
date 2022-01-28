using ChatAPI.Application.Contracts;
using ChatAPI.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatAPI.Application.CQRS.Commands
{
    public class AddFilesToMessageCommandHandler : IRequestHandler<AddFilesToMessageCommand, MessageForResponseDTO>
    {
        private readonly IChatService _chatService;

        public AddFilesToMessageCommandHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<MessageForResponseDTO> Handle(AddFilesToMessageCommand request, CancellationToken cancellationToken)
        {
            return await _chatService.AddFilesToMessage(request.UserId, request.MessageId, request.FormFiles);
        }
    }
}
