namespace WordFinder.Api.Features.FindWords;

public record FindWordsResponse(IReadOnlyCollection<WordDto> Words);
