using PostAPI.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostAPI.Application.CQRS.Queries
{
    public class GetRatesByPostsIdsQuery : IRequest<List<List<PostRateForResponseDTO>>>
    {
        public List<int> PostIds { get; set; }
    }
}
