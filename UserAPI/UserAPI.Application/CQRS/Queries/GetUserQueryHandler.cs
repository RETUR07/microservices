using UserAPI.Application.Contracts;
using UserAPI.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserAPI.Application.CQRS.Queries;

namespace UserAPI.Application.CQRS.Chat.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserForResponseDTO>
    {
        private readonly IUserService _userService;

        public GetUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserForResponseDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserAsync(request.UserId);
        }
    }
}
