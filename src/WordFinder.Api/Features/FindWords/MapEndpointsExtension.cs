//using FluentValidation;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;

//namespace WordFinder.Api.Features.FindWords;

//public static class MapEndpointsExtension
//{
//    public static WebApplication MapFindWords(this WebApplication app)
//    {
//        app.MapGet("/api/wordfinder", 
//            async ([FromServices] IMediator mediator,[FromQuery] FindWordsOptions options) => 
//                await mediator.Send(new FindWordsRequest(options.Letters)))
//        .WithName("Wordfinder")
//        .AllowAnonymous()
//        .Produces<FindWordsResponse>()
//        .WithOpenApi();

//        return app;
//    }

//}
