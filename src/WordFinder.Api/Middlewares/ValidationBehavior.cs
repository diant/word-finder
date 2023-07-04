using FluentValidation;
using MediatR;

namespace WordFinder.Api.Middlewares;

internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var validator = _validators.SingleOrDefault(v => typeof(IValidator<TRequest>).IsAssignableFrom(v.GetType())) ??
            throw new InvalidOperationException($"No validator found for {typeof(TRequest).Name}");
        var result = await validator.ValidateAsync(context, cancellationToken);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }

        return await next();
    }

}