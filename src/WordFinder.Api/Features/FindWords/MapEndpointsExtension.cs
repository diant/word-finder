using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WordFinder.Api.Features.FindWords;

public static class MapEndpointsExtension
{
    public static WebApplication MapFindWords(this WebApplication app)
    {
        app.MapGet("/api/wordfinder/{letters}", 
            async ([FromServices] IMediator mediator, string letters) => await mediator.Send(new FindWordsRequest(letters)))
        .WithName("Wordfinder")
        .AllowAnonymous()
        .Produces<FindWordsResponse>()
        //.Produces(StatusCodes.Status200OK)
        //.Produces(StatusCodes.Status400BadRequest)
        .WithOpenApi();

        return app;
    }
}