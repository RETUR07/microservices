using Microsoft.AspNetCore.Http;
using ChatAPI.Application.DTO;
using ChatAPI.Entities.Models;
using ChatAPI.Entities.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatAPI.Application.Contracts
{
    public interface IChatService
    {
        public Task<List<ChatForResponseDTO>> GetChats(string userId);
        public Task<List<MessageForResponseDTO>> GetMessages(string userId, int chatId);
        public Task<MessageForResponseDTO> GetMessage(int messageId);
        public Task<ChatForResponseDTO> GetChat(string userId, int chatId);
        public Task DeleteChat(int chatId);
        public Task DeleteMessage(string userId, int messageId);
        public Task<ChatForResponseDTO> CreateChat(string userId, ChatForm chatdto);
        public Task AddUser(int chatId, string userId, string adderId);
        public Task<MessageForResponseDTO> AddMessage(string UserId, MessageForm messagedto);
        public Task<MessageForResponseDTO> AddFilesToMessage(string UserId, int messageId, IEnumerable<IFormFile> formFiles);
    }
}
