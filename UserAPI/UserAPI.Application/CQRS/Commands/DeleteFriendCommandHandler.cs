using UserAPI.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace UserAPI.Application.CQRS.Commands
{
    public class DeleteFriendCommandHandler : IRequestHandler<DeleteFriendCommand>
    {
        private readonly IUserService _userService;

        public DeleteFriendCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Unit> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
        {
            await _userService.DeleteFriendAsync(request.UserId, request.FriendId);
            return Unit.Value;
        }
    }
}
