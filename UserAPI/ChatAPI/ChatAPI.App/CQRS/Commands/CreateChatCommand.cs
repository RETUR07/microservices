using ChatAPI.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Application.CQRS.Chat.Commands
{
    public class CreateChatCommand : IRequest<ChatForResponseDTO>
    {
        public string UserId { get; set; }
        public ChatForm ChatForm { get; set; }
    }
}
