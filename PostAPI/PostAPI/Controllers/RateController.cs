using Microsoft.AspNetCore.Mvc;
using PostAPI.Application.Contracts;
using PostAPI.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using PostAPI.Hubs;
using Microsoft.AspNetCore.Identity;
using PostAPI.Entities.Models;
using MediatR;
using System;
using PostAPI.Application.CQRS.Commands;
using PostAPI.Application.CQRS.Queries;

namespace PostAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : Base
    {
        IHubContext<RateHub> _hubContext;
        private readonly IMediator _mediator;

        public RateController(IMediator mediator, IHubContext<RateHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpGet("post/{userId}/{postId}")]
        public async Task<IActionResult> GetRate(string userId, int postId)
        {
            try
            {
                return Ok(await _mediator.Send(new GetPostRateQuery() { UserId = userId, PostId = postId }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetRatesOfPost(int postId)
        {
            try
            {
                return Ok(await _mediator.Send(new GetRatesByPostIdQuery() { PostId = postId }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("posts")]
        public async Task<IActionResult> GetRatesOfPost([FromQuery(Name = "postIDs")] List<int> postIds)
        {
            try
            {
                return Ok(await _mediator.Send(new GetRatesByPostsIdsQuery() { PostIds = postIds }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("post")]
        public async Task<IActionResult> UpdatePostRate([FromBody]RateForm rateForm)
        {
            try
            {
                await _mediator.Send(new UpdatePostRateCommand() { RateForm = rateForm, UserId = UserId });
                await _hubContext.Clients.All.SendAsync("Notify", await _mediator.Send(new GetPostRateQuery() { UserId = UserId, PostId = rateForm.ObjectId }));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
