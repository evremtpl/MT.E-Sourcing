using AutoMapper;
using MediatR;
using MT.E_Sourcing.Common.Events.Concrete;
using MT.E_Sourcing.Common.RabbitMq.Connection.Interfaces;
using MT.E_Sourcing.Common.RabbitMq.Core;
using MT.E_Sourcing.Order.Application.Commands.OrderCreate;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Order.Consumers
{
    public class EventBusOrderCreateConsumer
    {
        private readonly IRabbitMqPersistentConnection _persistentConnection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EventBusOrderCreateConsumer(IRabbitMqPersistentConnection persistentConnection, IMediator mediator, IMapper mapper)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void Consume()
        {
            if(!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            var channel = _persistentConnection.CreateModel();

            channel.QueueDeclare(queue: EventBusConstants.OrderCreateQueue, durable: 
                false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.OrderCreateQueue,autoAck:true,consumer:consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.Span);
            var @event = JsonConvert.DeserializeObject<OrderCreateEvent>(message);

            if(e.RoutingKey==EventBusConstants.OrderCreateQueue)
            {
                var command = _mapper.Map<OrderCreateCommand>(@event);
                command.CreateDate = DateTime.Now;

                command.TotalPrice = @event.Quantity * @event.Price;
                command.UnitePrice = @event.Price;

                var result = await _mediator.Send(command);
            }
        }

        public void Disconnect()
        {
            _persistentConnection.Dispose();
        }
    }
}
