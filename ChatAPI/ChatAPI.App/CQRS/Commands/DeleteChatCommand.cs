using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Application.CQRS.Commands
{
    public class DeleteChatCommand : IRequest
    {
        public int ChatId { get; set; }
    }
}
