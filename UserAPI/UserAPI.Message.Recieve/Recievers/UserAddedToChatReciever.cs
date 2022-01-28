using UserAPI.Entities.Models;
using UserAPI.Messaging.Recieve.Config;
using UserAPI.Messaging.Recieve.DTO;
using UserAPI.Repository.Contracts;
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

namespace UserAPI.Messaging.Recieve.Recievers
{
    public class UserAddedToChatReciever : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        private IModel _channel;
        private IConnection _connection;
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;

        public UserAddedToChatReciever(IOptions<RecieverRabbitMqConfiguration> rabbitMqOptions
            , IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName + "UserAdded";
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
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
                var recieved = JsonConvert.DeserializeObject<ChatForRecieveDTO>(content);

                await HandleMessage(recieved);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessage(ChatForRecieveDTO recieved)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IRepositoryManager _repositoryManager =
                    scope.ServiceProvider.GetRequiredService<IRepositoryManager>();
                var chat = _repositoryManager.Chat.FindByCondition(x => x.ChatId == recieved.ChatId, true).FirstOrDefault();
                foreach (var id in recieved.Users)
                {
                    var user = await _repositoryManager.User.GetUserAsync(id, true);
                    chat.Users.Add(user);
                }
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
