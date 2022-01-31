using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UserAPI.Application.Contracts;
using UserAPI.Application.DTO;
using UserAPI.Application.Mapping;
using UserAPI.Application.Services;
using UserAPI.Repository.Contracts;
using UserAPI.Repository.Repository;
using System;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UserAPI.Application.CQRS.Commands;
using UserAPI.Application.CQRS.Queries;
using System.Collections.Generic;
using System.Reflection;
using UserAPI.Messaging.Send.Sender;
using Microsoft.AspNetCore.Identity;
using UserAPI.Application.CQRS.Chat.Queries;

namespace SocialNetwork.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserMappingProfile));
        }
        public static void ConfigureServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IUserService, UserService>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton<IUserCreatedSender, UserCreatedSender>();
            services.AddSingleton<IUserDeletedSender, UserDeletedSender>();

            services.AddScoped<IRequestHandler<AddFriendCommand>, AddFriendCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteFriendCommand>, DeleteFriendCommandHandler>();
            services.AddScoped<IRequestHandler<CreateUserCommand, IdentityResult>, CreateUserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand>, DeleteUserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand>, UpdateUserCommandHandler>();
            services.AddScoped<IRequestHandler<GetUserQuery, UserForResponseDTO>, GetUserQueryHandler>();
            services.AddScoped<IRequestHandler<GetUsersQuery, List<UserForResponseDTO>>, GetUsersQueryHandler>();
        }

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<RepositoryContext>(opts =>
                    opts.UseSqlServer(Configuration.GetConnectionString("sqlConnection"), x => x.MigrationsAssembly("UserAPI")));
        }

        public static void ConfigureAuthorization(this IServiceCollection services, IConfiguration Configuration)
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
                options.Authority = Configuration.GetSection("Authority")["AuthorityURL"];
                                                                                          //

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                };
            }
            );
        }
    }
}
