using ChatAPI.Application.Contracts;
using ChatAPI.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatAPI.Application.CQRS.Chat.Commands
{
    public class AddMessageCommandHandler : IRequestHandler<AddMessageCommand, MessageForResponseDTO>
    {
        private readonly IChatService _chatService;

        public AddMessageCommandHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<MessageForResponseDTO> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            return await _chatService.AddMessage(request.UserId, request.MessageForm);
        }
    }
}
