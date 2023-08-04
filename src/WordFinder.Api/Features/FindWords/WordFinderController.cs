using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WordFinder.Api.Features.FindWords;

[ApiController]
[Route("api/[controller]")]
public sealed class WordFinderController : ControllerBase
{
    private readonly IMediator _mediator;

    public WordFinderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    //[Produces(typeof(FindWordsResponse))]
    public async Task<IActionResult> FindWords(FindWordsOptions options)
    {
        var response = await _mediator.Send(
            new FindWordsRequest(options.Letters, options.StartsWith, options.Contains, options.EndsWith, options.MinLen)
        );
        return Ok(response);
    }
}