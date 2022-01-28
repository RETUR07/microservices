using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Application.Contracts;
using UserAPI.Application.DTO;
using UserAPI.Entities.Models;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using System;
using UserAPI.Application.CQRS.Queries;
using UserAPI.Application.CQRS.Commands;

namespace UserAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Base
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok(await _mediator.Send(new GetUsersQuery()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("info", Name = "GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                return Ok(await _mediator.Send(new GetUserQuery() { UserId = UserId }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserRegistrationForm userdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
                }
                var result = await _mediator.Send(new CreateUserCommand() { UserForm = userdto });
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateUser([FromBody] UserForm userdto)
        {
            try
            {
                await _mediator.Send(new UpdateUserCommand() { UserId = UserId, UserForm = userdto });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteUserCommand() { UserId = UserId }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("addfriend/{friendId}")]
        public async Task<IActionResult> AddFriend(string friendId)
        {
            try
            {
                await _mediator.Send(new AddFriendCommand() { UserId = UserId, FriendId = friendId });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("deletefriend/{friendId}")]
        public async Task<IActionResult> DeleteFriend(string friendId)
        {

            try
            {
                await _mediator.Send(new DeleteFriendCommand() { UserId = UserId, FriendId = friendId });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
