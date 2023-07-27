using FluentAssertions;

namespace WordFinder.Core.UnitTests
{
    public class WordFinderTests
    {
        const int TotalWords = 267750;

        [Fact]
        public async Task WordsFileIsReadSuccess()
        {
            var words = await WordsReader.GetWordsAsync(7);
            words.Count().Should().Be(TotalWords);  
        }

        [Fact]
        public void LoadWordsAsEnumerable()
        {
            var words = WordsReader.LoadWordsFromFileAsEnumerable(7);
            words.Count().Should().Be(TotalWords);
        }
    }
}