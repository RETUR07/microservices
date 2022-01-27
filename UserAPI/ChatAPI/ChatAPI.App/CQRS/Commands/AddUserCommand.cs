using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Application.CQRS.Chat.Commands
{
    public class AddUserCommand : IRequest
    {
        public string UserId { get; set; }
        public string AdderId { get; set; }
        public int ChatId { get; set; }
    }
}
