using UserAPI.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI.Application.CQRS.Queries
{
    public class GetUserQuery : IRequest<UserForResponseDTO>
    {
        public string UserId { get; set; }
    }
}
