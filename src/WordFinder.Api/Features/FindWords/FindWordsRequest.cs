using MediatR;

namespace WordFinder.Api.Features.FindWords;

public record FindWordsRequest(string Letters, bool Grouped) : IRequest<FindWordsResponse>;
