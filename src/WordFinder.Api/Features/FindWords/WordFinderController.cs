﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WordFinder.Api.Features.FindWords;

public record FindWordsOptions(string Letters);

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
        var response = await _mediator.Send(new FindWordsRequest(options.Letters));
        return Ok(response);
    }
}