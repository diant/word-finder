using MediatR;

namespace WordFinder.Api.Features.FindWords;

internal sealed class FindWordsHandler : IRequestHandler<FindWordsRequest, FindWordsResponse>
{
    public Task<FindWordsResponse> Handle(FindWordsRequest request, CancellationToken cancellationToken)
    {
        var dtos = new List<WordDto>();
        var words = Core.WordFinder.Find(request.Letters, request.Contains, request.StartsWith, request.EndsWith, request.MinLen);
        foreach (var word in words.OrderByDescending(x => x.Length))
        {
            dtos.Add(Mapper.Map(word));
        }
        return Task.FromResult(new FindWordsResponse(dtos));
    }
}
