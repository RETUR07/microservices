using UserAPI.Application.Contracts;
using UserAPI.Messaging.Send.DTO;
using UserAPI.Messaging.Send.Sender;
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
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IdentityResult>
    {
        private readonly IUserService _userService;
        private readonly IUserCreatedSender _userCreatedSender;

        public CreateUserCommandHandler(IUserService userService, IUserCreatedSender userCreatedSender)
        {
            _userService = userService;
            _userCreatedSender = userCreatedSender;
        }

        public async Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.CreateUserAsync(request.UserForm);
            if(result.Succeeded)
            {
                var user = _userService.GetUserByEmail(request.UserForm.Email);
                _userCreatedSender.Send(new UserForSendDTO() { UserId = user.Id });
            }
            return result;
        }
    }
}
