using MediatR;

namespace WordFinder.Api.Features.FindWords;

public record FindWordsRequest(string Letters, string? Contains = default) : IRequest<FindWordsResponse>;
