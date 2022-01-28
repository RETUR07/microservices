using PostAPI.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostAPI.Application.CQRS.Commands
{
    public class UpdatePostRateCommand : IRequest
    {
        public string UserId { get; set; }
        public RateForm RateForm { get; set; }
    }
}
