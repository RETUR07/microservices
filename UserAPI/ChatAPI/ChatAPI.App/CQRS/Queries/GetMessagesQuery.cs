using ChatAPI.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Application.CQRS.Chat.Queries
{
    public class GetMessagesQuery : IRequest<List<MessageForResponseDTO>>
    {
        public string UserId { get; set; }
        public int ChatId { get; set; }
    }
}
