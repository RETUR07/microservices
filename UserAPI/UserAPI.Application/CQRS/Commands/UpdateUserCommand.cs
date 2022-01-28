using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAPI.Application.DTO;

namespace UserAPI.Application.CQRS.Commands
{
    public class UpdateUserCommand : IRequest
    {
        public string UserId { get; set;}
        public UserForm UserForm { get; set; }
    }
}
