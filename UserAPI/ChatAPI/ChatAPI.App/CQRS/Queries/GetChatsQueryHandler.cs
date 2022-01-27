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
    public class GetChatsQueryHandler : IRequestHandler<GetChatsQuery, List<ChatForResponseDTO>>
    {
        private readonly IChatService _chatService;

        public GetChatsQueryHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<List<ChatForResponseDTO>> Handle(GetChatsQuery request, CancellationToken cancellationToken)
        {
            return await _chatService.GetChats(request.UserId);
        }
    }
}
