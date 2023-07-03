namespace WordFinder.Core.UnitTests
{
    public class WordFinderTests
    {
        [Fact]
        public void WordsFileIsReadSuccess()
        {
            var words = WordsReader.GetWords();
            var list = WordFinder.Find("zerobat");
            Assert.True(words.Any());
        }
    }
}