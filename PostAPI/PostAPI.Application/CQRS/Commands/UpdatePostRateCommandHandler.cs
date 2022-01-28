using PostAPI.Application.Contracts;
using PostAPI.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PostAPI.Application.CQRS.Commands
{
    public class UpdatePostRateCommandHandler : IRequestHandler<UpdatePostRateCommand>
    {
        private readonly IRateService _rateService;

        public UpdatePostRateCommandHandler(IRateService rateService)
        {
            _rateService = rateService;
        }

        public async Task<Unit> Handle(UpdatePostRateCommand request, CancellationToken cancellationToken)
        {
            await _rateService.UpdatePostRateAsync(request.RateForm, request.UserId);
            return Unit.Value;
        }
    }
}
