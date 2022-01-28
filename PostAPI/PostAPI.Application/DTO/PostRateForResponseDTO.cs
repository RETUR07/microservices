using PostAPI.Entities.Models;
using System;

namespace PostAPI.Application.DTO
{
    public class PostRateForResponseDTO
    {
        public string UserId { get; set; }
        public int PostId { get; set; }
        public string LikeStatus { get; set; }
    }
}
