using WordFinder.Core;

namespace WordFinder.Api.Features.FindWords;

public record FindWordsResponse(IReadOnlyCollection<Word> Words);
