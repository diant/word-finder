using WordFinder.Core;

namespace WordFinder.Api.Features.FindWords;

public static class Mapper
{
    public static WordDto Map(Word word) =>
        new WordDto(word.Value, word.Points, word.Length, word.WildcardIndex);
}
