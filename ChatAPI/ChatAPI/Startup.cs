using ChatAPI.Hubs;
using ChatAPI.Messaging.Recieve.Config;
using ChatAPI.Messaging.Recieve.Recievers;
using ChatAPI.Messaging.Send.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ChatAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI
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

            services.AddHostedService<UserCreatedReciever>();
            services.AddHostedService<UserDeletedReciever>();

            services.AddControllers();
            services.AddSignalR().AddStackExchangeRedis(Configuration.GetSection("SignalRCache")["Connection"]);

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
                endpoints.MapHub<ChatHub>("/hubs/chats").RequireAuthorization("ApiScope");
            });
        }
    }
}
