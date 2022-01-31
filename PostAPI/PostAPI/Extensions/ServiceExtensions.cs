using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PostAPI.Application.Contracts;
using PostAPI.Application.CQRS.Commands;
using PostAPI.Application.CQRS.Queries;
using PostAPI.Application.DTO;
using PostAPI.Application.Mapping;
using PostAPI.Application.Services;
using PostAPI.Entities.RequestFeatures;
using PostAPI.Messaging.Send.Sender;
using PostAPI.Repository.Contracts;
using PostAPI.Repository.Repository;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace PostAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(PostMappingProfile));
        }
        public static void ConfigureServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IBlobService, BlobService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IRateService, RateService>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton<IPostCreatedSender, PostCreatedSender>();
            services.AddSingleton<IPostDeletedSender, PostDeletedSender>();

            services.AddScoped<IRequestHandler<CreatePostCommand>, CreatePostCommandHandler>();
            services.AddScoped<IRequestHandler<DeletePostCommand>, DeletePostCommandHandler>();
            services.AddScoped<IRequestHandler<UpdatePostRateCommand>, UpdatePostRateCommandHandler>();
            services.AddScoped<IRequestHandler<GetChildPostsQuery, PagedList<PostForResponseDTO>>, GetChildPostsQueryHandler>();
            services.AddScoped<IRequestHandler<GetPostsQuery, PagedList<PostForResponseDTO>>, GetPostsQueryHandler>();
            services.AddScoped<IRequestHandler<GetPostQuery, PostForResponseDTO>, GetPostQueryHandler>();
            services.AddScoped<IRequestHandler<GetRatesByPostIdQuery, List<PostRateForResponseDTO>>, GetRatesByPostIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetRatesByPostsIdsQuery, List<List<PostRateForResponseDTO>>>, GetRatesByPostsIdsQueryHandler>();

        }

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<RepositoryContext>(opts =>
                    opts.UseSqlServer(Configuration.GetConnectionString("sqlConnection"), x => x.MigrationsAssembly("ChatAPI")));
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
                options.Authority = Configuration.GetSection("Authority")["AuthorityURL"];//host.docker.internal
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
