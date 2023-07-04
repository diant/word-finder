namespace WordFinder.Core.UnitTests
{
    public class WordFinderTests
    {
        [Fact]
        public void WordsFileIsReadSuccess()
        {
            var words = WordsReader.GetWords();
            Assert.True(words.Any());
        }       
    }
}