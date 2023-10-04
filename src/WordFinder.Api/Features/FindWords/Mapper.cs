using WordFinder.Core;

namespace WordFinder.Api.Features.FindWords;

internal static class Mapper
{
    internal static WordDto Map(Word word) =>
        new(word.Value, word.Points, word.Length, word.WildcardIndex);
}
