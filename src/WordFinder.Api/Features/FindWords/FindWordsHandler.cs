﻿using MediatR;

namespace WordFinder.Api.Features.FindWords;

internal sealed class FindWordsHandler : IRequestHandler<FindWordsRequest, FindWordsResponse>
{
    public async Task<FindWordsResponse> Handle(FindWordsRequest request, CancellationToken cancellationToken)
    {
        var words = await Core.WordFinder.Find(request.Letters, request.Grouped);
        return new FindWordsResponse(words.SelectMany(x => x.Value).ToList());
    }
}