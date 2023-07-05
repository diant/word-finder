using MediatR;

namespace WordFinder.Api.Features.FindWords;

internal sealed class FindWordsHandler : IRequestHandler<FindWordsRequest, FindWordsResponse>
{
    public async Task<FindWordsResponse> Handle(FindWordsRequest request, CancellationToken cancellationToken)
    {
        var words = await Core.WordFinder.Find(request.Letters);
        return new FindWordsResponse(words.OrderByDescending(x => x.Length).Select(x => x.Value).ToList());
    }
}
