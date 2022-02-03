﻿using PostAPI.Entities.Models;
using PostAPI.Messaging.Recieve.Config;
using PostAPI.Messaging.Recieve.DTO;
using PostAPI.Repository.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PostAPI.Messaging.Recieve.Recievers
{
    public class UserCreatedReciever : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        private IModel _channel;
        private IConnection _connection;
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;
        private readonly int _port;

        public UserCreatedReciever(IOptions<RecieverRabbitMqConfiguration> rabbitMqOptions
            , IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName + "UserCreated";
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _port = rabbitMqOptions.Value.Port;
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                Port = _port,
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var recieved = JsonConvert.DeserializeObject<UserDTO>(content);

                await HandleMessage(recieved);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessage(UserDTO recieved)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IRepositoryManager _repositoryManager =
                    scope.ServiceProvider.GetRequiredService<IRepositoryManager>();
                 _repositoryManager.User.Create(new User() { IsEnable = true, UserId = recieved.UserId });
                await _repositoryManager.SaveAsync();
            }
            
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
