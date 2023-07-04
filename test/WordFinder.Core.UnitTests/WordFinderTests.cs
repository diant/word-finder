namespace WordFinder.Core.UnitTests
{
    public class WordFinderTests
    {
        [Fact]
        public async Task WordsFileIsReadSuccess()
        {
            var words = await WordsReader.GetWords();
            Assert.True(words.Any());
        }       
    }
}