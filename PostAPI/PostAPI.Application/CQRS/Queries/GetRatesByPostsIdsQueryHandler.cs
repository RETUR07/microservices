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
    public class GetRatesByPostsIdsQueryHandler : IRequestHandler<GetRatesByPostsIdsQuery, List<List<PostRateForResponseDTO>>>
    {
        private readonly IRateService _rateService;

        public GetRatesByPostsIdsQueryHandler(IRateService rateService)
        {
            _rateService = rateService;
        }

        public async Task<List<List<PostRateForResponseDTO>>> Handle(GetRatesByPostsIdsQuery request, CancellationToken cancellationToken)
        {
            return await _rateService.GetRatesByPostsIdsAsync(request.PostIds);
        }
    }
}
