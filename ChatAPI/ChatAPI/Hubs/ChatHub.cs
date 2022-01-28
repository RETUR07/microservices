using ChatAPI.Application.Contracts;
using ChatAPI.Application.DTO;
using ChatAPI.Application.Exceptions;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatAPI.Hubs
{
    public class ChatHub : BaseHub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task Subscribe(int chatId)
        {
            if (await _chatService.GetChat(UserId, chatId) == null)
            {
                await Clients.Caller.SendAsync("Notify", "Denied");
            }
            else
            {
                string groupname = "chat" + chatId;
                await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
                await Clients.Caller.SendAsync("Notify", "Subscribed");
            }
        }

        public async Task AddMessage(MessageForm messagedto)
        {
            try
            {
                var message = await _chatService.AddMessage(UserId, messagedto);
                await Clients.Group("chat" + message.ChatId).SendAsync("Send", message);
            }
            catch (InvalidDataException exc)
            {
                await Clients.Caller.SendAsync(exc.Message);
            }
        }

        public async Task MessageChanged(int messageId)
        {
            try
            {
                var message = await _chatService.GetMessage(messageId);
                await Clients.Group("chat" + message.ChatId).SendAsync("MessageChanged", message);
            }
            catch (InvalidDataException exc)
            {
                await Clients.Caller.SendAsync(exc.Message);
            }
        }
    }
}
