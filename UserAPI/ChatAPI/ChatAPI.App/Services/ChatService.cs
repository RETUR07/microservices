using AutoMapper;
using Microsoft.AspNetCore.Http;
using ChatAPI.Application.Contracts;
using ChatAPI.Application.DTO;
using ChatAPI.Application.Exceptions;
using ChatAPI.Entities.Models;
using ChatAPI.Entities.RequestFeatures;
using ChatAPI.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IBlobService _blobService;

        public ChatService(IRepositoryManager repository, IMapper mapper, IBlobService blobService)
        {
            _repository = repository;
            _mapper = mapper;
            _blobService = blobService;

        }

        public async Task<MessageForResponseDTO> AddFilesToMessage(string UserId, int messageId, IEnumerable<IFormFile> formFiles)
        {
            var message = await _repository.Message.GetMessageAsync(messageId, true);
            if (message == null || message.UserId != UserId) return null;
            message.Blobs = await _blobService.SaveBlobsAsync(formFiles, message.Chat.Id + "-message-" + message.Id);
            return await GetMessage(message.Id);
        }

        public async Task<MessageForResponseDTO> AddMessage(string userId, MessageForm messagedto)
        {
            if (messagedto == null) throw new InvalidDataException("message dto is null");
            var chat = await _repository.Chat.GetChatAsync(messagedto.ChatId, true);
            if (chat == null) throw new InvalidDataException("no such chat");
            var message = _mapper.Map<MessageForm, Message>(messagedto);
            message.UserId = userId;
            chat.Messages.Add(message);
            await _repository.SaveAsync();
            message.Blobs = await _blobService.SaveBlobsAsync(messagedto.Content, messagedto.ChatId + "-message-" + message.Id);
            await _repository.SaveAsync();
            return await GetMessage(message.Id);
        }

        public async Task AddUser(int chatId, string userId, string adderId)
        {
            var chat = await _repository.Chat.GetChatAsync(chatId, true);
            if (chat == null) throw new InvalidDataException("no such chat");
            if (!chat.UserIds.Select(x => x.UserId).Contains(adderId))
                return;
            else
                if(userId == adderId)
                    chat.UserIds.Remove(new User() { UserId = userId });
                else
                    chat.UserIds.Add(new User() { UserId = userId });
            await _repository.SaveAsync();
        }

        public async Task<ChatForResponseDTO> CreateChat(string userId, ChatForm chatdto)
        {
            if (chatdto == null || chatdto.Users == null)
            {
                return null;
            }
            var chat = new Chat
            {
                UserIds = new List<User>()
            };
            if (!chatdto.Users.Contains(userId))
            {
                var user = new User() { UserId = userId };
                chat.UserIds.Add(user);
            }
            
            foreach (var x in chatdto.Users)
            {
                var user = new User() { UserId = x};
                chat.UserIds.Add(user);
            }
            _repository.Chat.Create(chat);
            await _repository.SaveAsync();
            var response = _mapper.Map<ChatForResponseDTO>(chat);
            return response;
        }

        public async Task DeleteChat(int chatId)
        {
            var chat = await _repository.Chat.GetChatAsync(chatId, true);
            if (chat != null && chat.Messages != null)
                foreach(var x in chat.Messages)
                {
                    _repository.Message.Delete(x);
                }
            _repository.Chat.Delete(chat);
            await _repository.SaveAsync();
        }

        public async Task DeleteMessage(string userId, int messageId)
        {
            var message = await _repository.Message.GetMessageAsync(messageId, true);
            if (message.UserId == userId)
            {
                _repository.Message.Delete(message);
                await _repository.SaveAsync();
            }
        }

        public async Task<ChatForResponseDTO> GetChat(string userId, int chatId)
        {
            var chat = await _repository.Chat.GetChatAsync(chatId, false);

            if (chat == null || !chat.UserIds.Select(x => x.UserId).Contains(userId))
            {
                return null;
            }
            

            var chatdto = _mapper.Map<ChatForResponseDTO>(chat);
            for (int idx = 0; idx < chat.Messages.Count; idx++)
            {
                if(chat.Messages[idx].Blobs != null)
                chatdto.Messages[idx].Content = 
                    await _blobService.GetBLobsAsync(chat.Messages[idx].Blobs.Select(x => x.Id), false);
            }
            return chatdto;
        }

        public async Task<List<ChatForResponseDTO>> GetChats(string userId)
        {
            var chats = await _repository.Chat.GetChatsAsync(userId, false);
            if (chats == null)
            {
                return null;
            }
            var chatdto = _mapper.Map<List<ChatForResponseDTO>>(chats);
            return chatdto;
        }

        public async Task<MessageForResponseDTO> GetMessage(int messageId)
        {
            var message = await _repository.Message.GetMessageAsync(messageId, false);
            if (message == null)
            {
                return null;
            }
            var messagedto = _mapper.Map<MessageForResponseDTO>(message);
            messagedto.Content = await _blobService.GetBLobsAsync(message.Blobs.Select(x => x.Id), false);
            return messagedto;
        }

        public async Task<List<MessageForResponseDTO>> GetMessages(string userId, int chatId)
        {
            var chat = await _repository.Chat.GetChatAsync(chatId, false);

            if (!chat.UserIds.Select(x => x.UserId).Contains(userId)) return null;
            var messages = await _repository.Message.GetMessgesByChatIdAsync(chatId, false);
            if (messages == null)
            {
                return null;
            }
            var messagesdto = _mapper.Map<List<MessageForResponseDTO>>(messages);
            for(int i = 0; i < messages.Count; i++)
            {
                messagesdto[i].Content = await _blobService.GetBLobsAsync(messages[i].Blobs.Select(x => x.Id), false);
            }
            return messagesdto;
        }
    }
}
