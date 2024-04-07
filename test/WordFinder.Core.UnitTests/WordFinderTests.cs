using FluentAssertions;

namespace WordFinder.Core.UnitTests
{
    public class WordFinderTests
    {
        const int TotalWords = 74414; // words with length more than 2 and max 7 characters

        [Fact]
        public void LoadWordsAsEnumerable()
        {
            var words = WordsReader.LoadWordsFromFileAsEnumerable(7);
            words.Count().Should().Be(TotalWords);
        }
    }
}