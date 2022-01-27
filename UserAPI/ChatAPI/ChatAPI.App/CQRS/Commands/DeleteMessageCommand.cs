using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Application.CQRS.Chat.Commands
{
    public class DeleteMessageCommand : IRequest
    {
        public string UserId { get; set; }
        public int MessageId { get; set; }
    }
}
