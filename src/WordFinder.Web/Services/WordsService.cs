using WordFinder.Web.Models;

namespace WordFinder.Web.Services
{
    public interface IWordsService
    {
        Task<WordsViewModel> FindWordsAsync(SearchOptions options);
    }

    public record SearchOptions(string Letters)
    {
    }

    public sealed class WordsService : IWordsService
    {
        public async Task<WordsViewModel> FindWordsAsync(SearchOptions options)
        {
            var words = await Core.WordFinder.Find(options.Letters);

            return new(
                words.
                    GroupBy(x => x.Length)
                    .Select(x => new WordGroup(
                        $"{x.Key} letters",
                        x.Select(x => new Word(x.Value, x.Points)).OrderBy(x => x.Value).ToArray()))
                    .ToList());
        }
    }
}
