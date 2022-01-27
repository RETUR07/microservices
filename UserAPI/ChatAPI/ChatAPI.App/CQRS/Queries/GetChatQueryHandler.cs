using ChatAPI.Application.Contracts;
using ChatAPI.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatAPI.Application.CQRS.Chat.Queries
{
    public class GetChatQueryHandler : IRequestHandler<GetChatQuery, ChatForResponseDTO>
    {
        private readonly IChatService _chatService;

        public GetChatQueryHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<ChatForResponseDTO> Handle(GetChatQuery request, CancellationToken cancellationToken)
        {
            return await _chatService.GetChat(request.UserId, request.ChatId);
        }
    }
}
