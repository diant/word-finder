using MediatR;

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
        _logger.LogInformation("{Request}/{Value}", request.GetType().Name, request);
        var r = await next();
        _logger.LogInformation("{Request}", request.GetType().Name);
        return r;
    }
}
