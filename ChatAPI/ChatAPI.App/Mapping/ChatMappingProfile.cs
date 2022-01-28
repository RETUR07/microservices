using AutoMapper;
using ChatAPI.Application.DTO;
using ChatAPI.Application.Services;
using ChatAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Application.Mapping
{
    public class ChatMappingProfile : Profile
    {
        public ChatMappingProfile()
        {
            CreateMap<MessageForm, Message>()
                .ForMember(m => m.Blobs, opt => opt.MapFrom(x => x.Content.FormFilesToBlobs()));

            CreateMap<Message, MessageForResponseDTO>()
                .ForMember(m => m.From, opt => opt.MapFrom(x => x.UserId));

            CreateMap<Chat, ChatForResponseDTO>()
                .ForMember(ch => ch.Users, opt => opt.MapFrom(x => x.UserIds.Select(x => x.UserId)))
                .ForMember(ch => ch.Messages, opt => opt.MapFrom(x => x.Messages));
        }
    }
}
