using UserAPI.Messaging.Send.Config;
using UserAPI.Messaging.Send.DTO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Collections.Generic;

namespace UserAPI.Messaging.Send.Sender
{
    public class UserCreatedSender : IUserCreatedSender
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _queueName1;
        private readonly string _queueName2;
        private readonly string _exchangeName;
        private readonly string _username;
        private readonly int _port;
        private IConnection _connection;

        public UserCreatedSender(IOptions<SenderRabbitMqConfiguration> rabbitMqOptions)
        {
            _queueName1 = "Chat" + rabbitMqOptions.Value.QueueName + "UserCreated";
            _queueName2 = "Post" + rabbitMqOptions.Value.QueueName + "UserCreated";
            _exchangeName = "userCreated-exchange";
            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _port = rabbitMqOptions.Value.Port;

            CreateConnection();
        }

        public void Send(UserForSendDTO dto)
        {
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.ExchangeDeclare(_exchangeName, ExchangeType.Fanout, true);

                    channel.QueueDeclare(queue: _queueName1, durable: true, exclusive: false, autoDelete: false, arguments: new Dictionary<string, object>() { { "x-queue-type", "quorum" } });
                    channel.QueueDeclare(queue: _queueName2, durable: true, exclusive: false, autoDelete: false, arguments: new Dictionary<string, object>() { { "x-queue-type", "quorum" } });

                    channel.QueueBind(_queueName1, _exchangeName, "");
                    channel.QueueBind(_queueName2, _exchangeName, "");

                    var json = JsonConvert.SerializeObject(dto);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: _exchangeName, routingKey: "", basicProperties: null, body: body);
                }
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    Port = _port,
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }
    }
}