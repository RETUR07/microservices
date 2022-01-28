using ChatAPI.Application.Contracts;
using ChatAPI.Application.DTO;
using ChatAPI.Messaging.Send.DTO;
using ChatAPI.Messaging.Send.Sender;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatAPI.Application.CQRS.Commands
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, ChatForResponseDTO>
    {
        private readonly IChatService _chatService;
        private readonly IChatCreatedSender _chatCreatedSender;

        public CreateChatCommandHandler(IChatService chatService, IChatCreatedSender chatCreatedSender)
        {
            _chatService = chatService;
            _chatCreatedSender = chatCreatedSender;
        }

        public async Task<ChatForResponseDTO> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await _chatService.CreateChat(request.UserId, request.ChatForm);
            _chatCreatedSender.Send(new ChatForSenderDTO() { ChatId = chat.Id, Users = chat.Users});
            return chat;
        }
    }
}
