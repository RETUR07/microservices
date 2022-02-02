using AuthorizationAPI.Entities.Models;
using AuthorizationAPI.IdentityTokenServer;
using AuthorizationAPI.Repository.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuthorizationAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RepositoryContext>(opts =>
                    opts.UseSqlServer(Configuration.GetConnectionString("sqlConnection"), x => x.MigrationsAssembly("AuthorizationAPI")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer(
                options =>
                {
                }
            )
            .AddDeveloperSigningCredential()
            .AddInMemoryApiResources(ResourceManager.Apis)
            .AddInMemoryClients(ClientManager.Clients)
            .AddInMemoryApiScopes(ScopeManager.ApiScopes)
            .AddAspNetIdentity<User>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use((context, next) =>
            {
                context.Request.Protocol = "http";
                context.Request.Host = new HostString("host.docker.internal:30901");

                return next();
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.UseIdentityServer();
            
        }
    }
}
