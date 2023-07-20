using MediatR;

namespace WordFinder.Api.Features.FindWords;

public record FindWordsRequest(
    string Letters, 
    string? StartsWith = default,
    string? Contains = default,
    string? EndsWith = default
    ) : IRequest<FindWordsResponse>;
