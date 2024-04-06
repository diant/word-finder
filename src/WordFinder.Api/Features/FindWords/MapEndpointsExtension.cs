using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WordFinder.Api.Features.FindWords;

public static class MapEndpointsExtension
{
    public static WebApplication MapFindWordsEndpoints(this WebApplication app)
    {
        app.MapGet("/api/wordfinder",
            async ([FromServices] IMediator mediator, [AsParameters] FindWordsOptions options) =>
                 await mediator.Send(new FindWordsRequest(options.Letters, options.StartsWith, options.Contains, options.EndsWith, options.MinLen)))
        .WithName("Wordfinder")
        .AllowAnonymous()
        .Produces<FindWordsResponse>()
        .WithOpenApi();

        return app;
    }

}
