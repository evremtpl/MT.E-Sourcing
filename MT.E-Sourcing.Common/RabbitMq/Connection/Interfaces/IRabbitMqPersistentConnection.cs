using RabbitMQ.Client;
using System;

namespace MT.E_Sourcing.Common.RabbitMq.Connection.Interfaces
{
    public interface IRabbitMqPersistentConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}
