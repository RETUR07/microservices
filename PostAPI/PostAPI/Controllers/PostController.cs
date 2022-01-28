using Microsoft.AspNetCore.Mvc;
using PostAPI.Application.Contracts;
using PostAPI.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using PostAPI.Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using PostAPI.Entities.Models;
using MediatR;
using System;
using PostAPI.Application.CQRS.Commands;
using PostAPI.Application.CQRS.Queries;

namespace PostAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : Base
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("userposts/{userId}")]
        public async Task<IActionResult> GetUserPosts(string userId, [FromQuery] Parameters parameters)
        {
            try
            {
                return Ok(await _mediator.Send(new GetPostsQuery() { UserId = UserId, Parameters = parameters }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("childposts/{postId}")]
        public async Task<IActionResult> GetChildPosts(int postId, [FromQuery] Parameters parameters)
        {
            try
            {
                return Ok(await _mediator.Send(new GetChildPostsQuery() { PostId = postId, Parameters = parameters }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{postId}", Name = "GetPost")]
        public async Task<IActionResult> GetPost(int postId)
        {
            try
            {
                return Ok(await _mediator.Send(new GetPostQuery() { PostId = postId }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] PostForm postdto)
        {
            try
            {
                return Ok(await _mediator.Send(new CreatePostCommand() { PostDTO = postdto, UserId = UserId }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            try
            {
                await _mediator.Send(new DeletePostCommand() { PostId = postId, UserId = UserId });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
