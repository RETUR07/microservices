using UserAPI.Messaging.Recieve.Config;
using UserAPI.Messaging.Send.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Extensions;
using UserAPI.Messaging.Recieve.Recievers;
using UserAPI.Entities.Models;
using UserAPI.Repository.Repository;
using Microsoft.AspNetCore.Identity;

namespace UserAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            var senderServiceClientSettingsConfig = Configuration.GetSection("SenderRabbitMq");
            var recieverServiceClientSettingsConfig = Configuration.GetSection("RecieverRabbitMq");
            services.Configure<SenderRabbitMqConfiguration>(senderServiceClientSettingsConfig);
            services.Configure<RecieverRabbitMqConfiguration>(recieverServiceClientSettingsConfig);

            services.AddHostedService<ChatCreatedReciever>();
            services.AddHostedService<ChatDeletedReciever>();
            services.AddHostedService<PostCreatedReciever>();
            services.AddHostedService<PostDeletedReciever>();
            services.AddHostedService<UserAddedToChatReciever>();

            services.AddControllers();
            services.AddSignalR();

            services.ConfigureAutoMapper();
            services.ConfigureServices(Configuration);
            services.ConfigureDatabase(Configuration);
            services.AddSwaggerGen(c =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { jwtSecurityScheme, Array.Empty<string>() }
                    });
            });

            services.AddIdentityCore<User>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();

            services.ConfigureAuthorization(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.EnablePersistAuthorization();
                    options.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:3000", "http://localhost:5000", "https://localhost:5001")
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization("ApiScope");
            });
        }
    }
}
