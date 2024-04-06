namespace WordFinder.Api.Features.FindWords;

public sealed record FindWordsOptions(
    string Letters,
    FindWordsGroupOptions GroupOptions = FindWordsGroupOptions.Length,
    string? StartsWith = null,
    string? Contains = null,
    string? EndsWith = null,
    int MinLen = 2
);