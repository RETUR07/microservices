using ChatAPI.Application.Contracts;
using ChatAPI.Application.CQRS.Commands;
using ChatAPI.Application.CQRS.Queries;
using ChatAPI.Application.DTO;
using ChatAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : Base
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        { 
            _mediator = mediator;
        }

        [HttpGet("chats/")]
        public async Task<IActionResult> GetChats()
        {
            try
            {
                return Ok(await _mediator.Send(new GetChatsQuery() { UserId = UserId}));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("messages/{chatId}")]
        public async Task<IActionResult> GetMessages(int chatId)
        {
            try
            {
                return Ok(await _mediator.Send(new GetMessagesQuery() { UserId = UserId, ChatId = chatId }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{chatId}", Name = "GetChat")]
        public async Task<IActionResult> GetChat(int chatId)
        {
            try
            {
                return Ok(await _mediator.Send(new GetChatQuery() { UserId = UserId, ChatId = chatId}));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("chat/{chatId}")]
        public async Task<IActionResult> DeleteChat(int chatId)
        {
            try
            {
                await _mediator.Send(new DeleteChatCommand() { ChatId = chatId});
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("message/{messageId}")]
        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            try
            {
                await _mediator.Send(new DeleteMessageCommand() { UserId = UserId, MessageId = messageId});
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody]ChatForm chatdto)
        {
            try
            {
                return Ok(await _mediator.Send(new CreateChatCommand() { UserId = UserId, ChatForm = chatdto}));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{chatId}/adduser/{userId}")]
        public async Task<IActionResult> AddUser(int chatId, string userId)
        {
            try
            {
                await _mediator.Send(new AddUserCommand() { UserId = userId, AdderId = UserId, ChatId = chatId });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("addmessage")]
        public async Task<IActionResult> AddMessage([FromForm]MessageForm messagedto)
        {
            try
            {               
                return Ok(await _mediator.Send(new AddMessageCommand() { UserId = UserId, MessageForm = messagedto }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addFilesToMessage/{messageId}")]
        public async Task<IActionResult> Add(int messageId, [FromForm] IEnumerable<IFormFile> formFiles)
        {
            try
            {
                return Ok(await _mediator.Send(new AddFilesToMessageCommand() { UserId = UserId, MessageId = messageId, FormFiles = formFiles }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
