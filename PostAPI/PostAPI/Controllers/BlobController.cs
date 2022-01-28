using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PostAPI.Application.Contracts;
using PostAPI.Entities.Models;
using System.Threading.Tasks;

namespace PostAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BlobController : Base
    {
        private readonly IBlobService _blobService;

        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet("{blobId}")]
        public async Task<IActionResult> GetChildPosts(int blobId)
        {
            var blob = await _blobService.GetBlobAsync(blobId);
            return File(blob.Data, blob.ContentType, blob.Filename);
        }
    }
}
