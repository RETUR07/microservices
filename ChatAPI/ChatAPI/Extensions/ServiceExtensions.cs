using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ChatAPI.Application.Contracts;
using ChatAPI.Application.DTO;
using ChatAPI.Application.Mapping;
using ChatAPI.Application.Services;
using ChatAPI.Repository.Contracts;
using ChatAPI.Repository.Repository;
using System;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ChatAPI.Application.CQRS.Commands;
using ChatAPI.Application.CQRS.Queries;
using System.Collections.Generic;
using System.Reflection;
using ChatAPI.Messaging.Send.Sender;

namespace SocialNetwork.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(ChatMappingProfile));
        }
        public static void ConfigureServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IBlobService, BlobService>();
            services.AddScoped<IChatService, ChatService>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton<IChatCreatedSender, ChatCreatedSender>();
            services.AddSingleton<IChatDeletedSender, ChatDeletedSender>();
            services.AddSingleton<IUserAddedToChatSender, UserAddedToChatSender>();

            services.AddScoped<IRequestHandler<AddFilesToMessageCommand, MessageForResponseDTO>, AddFilesToMessageCommandHandler>();
            services.AddScoped<IRequestHandler<AddMessageCommand, MessageForResponseDTO>, AddMessageCommandHandler>();
            services.AddScoped<IRequestHandler<AddUserCommand>, AddUserCommandHandler>();
            services.AddScoped<IRequestHandler<CreateChatCommand, ChatForResponseDTO>, CreateChatCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteChatCommand>, DeleteChatCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteMessageCommand>, DeleteMessageCommandHandler>();
            services.AddScoped<IRequestHandler<GetChatQuery, ChatForResponseDTO>, GetChatQueryHandler>();
            services.AddScoped<IRequestHandler<GetChatsQuery, List<ChatForResponseDTO>>, GetChatsQueryHandler>();
            services.AddScoped<IRequestHandler<GetMessagesQuery, List<MessageForResponseDTO>>, GetMessagesQueryHandler>();
        }

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<RepositoryContext>(opts =>
                    opts.UseSqlServer(Configuration.GetConnectionString("sqlConnection"), x => x.MigrationsAssembly("ChatAPI")));
        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "SocialNetwork");
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

            .AddJwtBearer(options =>
                {
                    //development
                    options.RequireHttpsMetadata = false;
                    options.Authority = "https://localhost:9001";//host.docker.internal
                    //

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/hubs") || path.StartsWithSegments("/api/Blob")))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                }
            );
        }
    }
}
