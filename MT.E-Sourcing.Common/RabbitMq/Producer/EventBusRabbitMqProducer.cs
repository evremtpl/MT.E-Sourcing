using Microsoft.Extensions.Logging;
using MT.E_Sourcing.Common.Events.Abstract;
using MT.E_Sourcing.Common.RabbitMq.Connection.Interfaces;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Net.Sockets;
using System.Text;

namespace MT.E_Sourcing.Common.RabbitMq.Producer
{
    public class EventBusRabbitMqProducer
    {

        private readonly IRabbitMqPersistentConnection _persistentConnection;
        private readonly ILogger<EventBusRabbitMqProducer> _logger;

        private readonly int _retryCount;

        public EventBusRabbitMqProducer(IRabbitMqPersistentConnection persistentConnection, ILogger<EventBusRabbitMqProducer> logger, int retryCount)
        {
            _persistentConnection = persistentConnection;
            _logger = logger;
            _retryCount = retryCount;
        }

        public void Publish(string queueName, IEvent @event)
        {
            if(!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var policy = RetryPolicy.Handle<SocketException>().Or<BrokerUnreachableException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, "RabbitMq Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex);
                });

            using(var channel =_persistentConnection.CreateModel())
            {
                channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);



                policy.Execute(() => {

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    properties.DeliveryMode = 2;

                    channel.ConfirmSelect();

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queueName,
                        mandatory: true,
                        basicProperties: properties,
                        body: body);


                    channel.WaitForConfirmsOrDie();
                    channel.BasicAcks += (sender, eventArgs) =>
                    {

                        Console.WriteLine("Sent RabbitMq");

                        //d,sgd
                    };

                });

            }
      
        }
    }
}
