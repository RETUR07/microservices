using MediatR;
using PostAPI.Application.Contracts;
using PostAPI.Application.CQRS.Queries;
using PostAPI.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PostAPI.Application.CQRS.Queries
{
    public class GetPostRateQueryHandler : IRequestHandler<GetPostRateQuery, PostRateForResponseDTO>
    {
        private readonly IRateService _rateService;

        public GetPostRateQueryHandler(IRateService rateService)
        {
            _rateService = rateService;
        }

        public async Task<PostRateForResponseDTO> Handle(GetPostRateQuery request, CancellationToken cancellationToken)
        {
            return await _rateService.GetPostRateAsync(request.UserId, request.PostId);
        }
    }
}
