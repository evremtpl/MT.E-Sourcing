using Microsoft.Extensions.Logging;
using MT.E_Sourcing.Common.RabbitMq.Connection.Interfaces;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.IO;
using System.Net.Sockets;

namespace MT.E_Sourcing.Common.RabbitMq.Connection.Concrete
{
    public class RabbitMqPersistentConnection : IRabbitMqPersistentConnection
    {

        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private readonly int _retryCount;
        private readonly ILogger<RabbitMqPersistentConnection> _logger;
        private bool _disposed;

        public RabbitMqPersistentConnection(IConnectionFactory connectionFactory, int retryCount, ILogger<RabbitMqPersistentConnection> logger)
        {
            _connectionFactory = connectionFactory;
            _retryCount = retryCount;
            _logger = logger;
        }

        public bool IsConnected
        {

            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;

            }

        }

        public bool TryConnect()
        {
            _logger.LogInformation("RabbitMq Client is trying to connect");

            var policy = RetryPolicy.Handle<SocketException>().Or<BrokerUnreachableException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                   {
                       _logger.LogWarning(ex, "RabbitMq Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex);
                   });

            policy.Execute(()=>
            {
                _connection = _connectionFactory.CreateConnection();
            });


            if(IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutDown;
                _connection.CallbackException +=OnCallBackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;

                _logger.LogInformation("RabbitMq acquired a persistent connection to '{HostName}' and is subcribed to failure events", _connection);
                return true;

            }
            else
            {
                _logger.LogCritical("FATAL ERROR :  RabbitMq connections could not be created and opened");
                return false;
            }

        }

        private void OnCallBackException(object sender, CallbackExceptionEventArgs e)
        {

            if (_disposed) return;

            _logger.LogWarning("A RabbitMq Connection throw exception. Trying to re-connect..");


            TryConnect();
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMq Connection is shutdown. Trying to re-connect..");

            TryConnect();
        }

        private void OnConnectionShutDown(object sender, ShutdownEventArgs e)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMq Connection is shutdown. Trying to re-connect..");

            TryConnect();
        }

        public IModel CreateModel()
        {
            if (!IsConnected) { throw new InvalidOperationException("No RabbitMq Connections are available to perform this action"); }
            return _connection.CreateModel();

        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException ex)
            {

                _logger.LogCritical(ex.ToString());
            }
        }

       
    }
}
