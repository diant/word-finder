namespace WordFinder.Api.Features.FindWords;

public record FindWordsOptions(
    string Letters, 
    FindWordsGroupOptions GroupOptions = FindWordsGroupOptions.Length,
    string? StartsWith = default,
    string? Contains = default,
    string? EndsWith = default,
    int MinLen = 2
);
