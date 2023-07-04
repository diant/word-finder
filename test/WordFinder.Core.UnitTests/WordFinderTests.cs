namespace WordFinder.Core.UnitTests
{
    public class WordFinderTests
    {
        [Fact]
        public void WordsFileIsReadSuccess()
        {
            var words = WordsReader.GetWords();
            var list = WordFinder.Find("zerobat", true);
            Assert.True(words.Any());
        }
    }
}