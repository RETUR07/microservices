using UserAPI.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserAPI.Messaging.Send.Sender;
using UserAPI.Messaging.Send.DTO;

namespace UserAPI.Application.CQRS.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserService _userService;
        private readonly IUserDeletedSender _userDeletedSender;


        public DeleteUserCommandHandler(IUserService userService, IUserDeletedSender userDeletedSender)
        {
            _userService = userService;
            _userDeletedSender = userDeletedSender;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userService.DeleteUserAsync(request.UserId);
            _userDeletedSender.Send(new UserForSendDTO() { UserId = request.UserId });
            return Unit.Value;
        }
    }
}
