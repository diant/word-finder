using WordFinder.Web.Models;

namespace WordFinder.Web.Services
{
    public interface IWordsService
    {
        WordsViewModel FindWords(SearchOptions options);
    }

    public sealed record SearchOptions(string Letters, int MinLength, string Contains, string StartsWith, string EndsWith);

    public sealed class WordsService : IWordsService
    {
        private const string DEF_URL = "https://www.wordreference.com/definition/";

        public WordsViewModel FindWords(SearchOptions options)
        {
            var words = Core.WordFinder
                .Find(options.Letters, options.Contains, options.StartsWith, options.EndsWith, options.MinLength);

            return new(
                options.Letters,
                words.
                    GroupBy(x => x.Length)
                    .Select(x => new WordGroup(
                        $"{x.Key} letters",
                        [.. x.Select(x => new Word(x.Value, x.Points, x.WildcardIndex, $"{DEF_URL}{x.Value}")).OrderBy(x => x.Value)]))
                    .ToList(),
                2);
        }
    }
}
