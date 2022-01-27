using ChatAPI.Application.Contracts;
using ChatAPI.Messaging.Send.DTO;
using ChatAPI.Messaging.Send.Sender;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatAPI.Application.CQRS.Chat.Commands
{
    public class DeleteChatCommandHandler : IRequestHandler<DeleteChatCommand>
    {
        private readonly IChatService _chatService;
        private readonly IChatDeletedSender _chatDeletedSender;

        public DeleteChatCommandHandler(IChatService chatService, IChatDeletedSender chatDeletedSender)
        {
            _chatService = chatService;
            _chatDeletedSender = chatDeletedSender;
        }

        public async Task<Unit> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
        {
            await _chatService.DeleteChat(request.ChatId);
            _chatDeletedSender.Send(new ChatForSenderDTO() { ChatId = request.ChatId });
            return Unit.Value;
        }
    }
}
