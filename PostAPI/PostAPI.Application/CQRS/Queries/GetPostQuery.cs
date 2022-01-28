using PostAPI.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostAPI.Entities.RequestFeatures;

namespace PostAPI.Application.CQRS.Queries
{
    public class GetPostQuery : IRequest<PostForResponseDTO>
    {
        public int PostId { get; set;}
    }
}
