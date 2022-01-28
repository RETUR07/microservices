using ChatAPI.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Application.CQRS.Commands
{
    public class AddMessageCommand : IRequest<MessageForResponseDTO>
    {
        public string UserId { get; set; }
        public MessageForm MessageForm { get; set; }
    }
}
