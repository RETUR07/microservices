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
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
    {
        private readonly IChatService _chatService;
        private readonly IUserAddedToChatSender _userAddedToChatSender;

        public AddUserCommandHandler(IChatService chatService, IUserAddedToChatSender userAddedToChatSender)
        {
            _chatService = chatService;
            _userAddedToChatSender = userAddedToChatSender;
        }

        public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            await _chatService.AddUser(request.ChatId, request.UserId, request.AdderId);
            _userAddedToChatSender.Send(new ChatForSenderDTO() { Users = { request.UserId }, ChatId = request.ChatId });
            return Unit.Value;
        }
    }
}
