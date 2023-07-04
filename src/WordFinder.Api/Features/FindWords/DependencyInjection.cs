using MediatR;
using Microsoft.AspNetCore.Mvc;
using WordFinder.Api.Middlewares;

namespace WordFinder.Api.Features.FindWords;

public static class DependencyInjection
{
    public static IServiceCollection AddWordFinder(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining<Program>();
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }

    public static WebApplication MapWordFinderEndpoints(this WebApplication app)
    {
        app.MapGet("/wordfinder",
            async ([FromServices] IMediator mediator, string letters, bool grouped = true) =>
            {
                return await mediator.Send(new FindWordsRequest(letters, grouped));
            })
        .WithName("Wordfinder")
        .AllowAnonymous()
        .Produces<FindWordsResponse>()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithOpenApi();

        return app;
    }
}