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
    public class AddFriendCommandHandler : IRequestHandler<AddFriendCommand>
    {
        private readonly IUserService _userService;

        public AddFriendCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Unit> Handle(AddFriendCommand request, CancellationToken cancellationToken)
        {
            await _userService.AddFriendAsync(request.UserId, request.FriendId);
            return Unit.Value;
        }
    }
}
