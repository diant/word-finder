using MediatR;

namespace WordFinder.Api.Features.FindWords;

internal sealed class FindWordsHandler : IRequestHandler<FindWordsRequest, FindWordsResponse>
{
    public async Task<FindWordsResponse> Handle(FindWordsRequest request, CancellationToken cancellationToken)
    {
        var words = await Core.WordFinder.FindAsync(request.Letters, request.Contains, request.StartsWith, request.EndsWith);
        return new FindWordsResponse(words.OrderByDescending(x => x.Length).ToList());
    }
}
