using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Order.Application.PipelineBehaviors
{
    public  class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehaviour( ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();
            var response = await next();

            var elapsedMilliSeconds = _timer.ElapsedMilliseconds;

            if(elapsedMilliSeconds >500)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogWarning("Long Running Request : {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                    requestName, elapsedMilliSeconds, request);
            }

            return response;
        }
    }
}
