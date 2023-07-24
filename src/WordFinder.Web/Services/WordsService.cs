using WordFinder.Web.Models;

namespace WordFinder.Web.Services
{
    public interface IWordsService
    {
        Task<WordsViewModel> FindWordsAsync(SearchOptions options);
    }

    public record SearchOptions(string Letters, int MinLength, string Contains, string StartsWith, string EndsWith);

    public sealed class WordsService : IWordsService
    {
        public async Task<WordsViewModel> FindWordsAsync(SearchOptions options)
        {
            var words = await Core.WordFinder
                .Find(options.Letters, options.Contains, options.StartsWith, options.EndsWith, options.MinLength);

            return new(
                options.Letters,
                words.
                    GroupBy(x => x.Length)
                    .Select(x => new WordGroup(
                        $"{x.Key} letters",
                        x.Select(x => new Word(x.Value, x.Points)).OrderBy(x => x.Value).ToArray()))
                    .ToList(),
                options.MinLength);
        }
    }
}
