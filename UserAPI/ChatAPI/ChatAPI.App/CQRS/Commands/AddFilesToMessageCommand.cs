using ChatAPI.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Application.CQRS.Chat.Commands
{
    public class AddFilesToMessageCommand : IRequest<MessageForResponseDTO>
    {
        public string UserId { get; set; }
        public int MessageId { get; set; }
        public IEnumerable<IFormFile> FormFiles { get; set; }
    }
}
