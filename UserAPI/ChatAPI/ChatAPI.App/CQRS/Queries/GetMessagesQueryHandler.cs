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
    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, List<MessageForResponseDTO>>
    {
        private readonly IChatService _chatService;

        public GetMessagesQueryHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<List<MessageForResponseDTO>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            return await _chatService.GetMessages(request.UserId, request.ChatId);
        }
    }
}
