using MediatR;
using System.Diagnostics;

namespace WordFinder.Api.Middlewares;

internal class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{Message}/{Request}/{Value}", 
            "Handling request", request.GetType().Name, request);

        Stopwatch sw = new();
        sw.Start();
        var r = await next();
        sw.Stop();

        _logger.LogInformation("{Message}/{Request}",
            $"Request handled in {sw.ElapsedMilliseconds}ms", request.GetType().Name);

        return r;
    }
}
