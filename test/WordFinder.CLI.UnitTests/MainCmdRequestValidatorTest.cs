using WordFinder.CLI.Commands;

namespace WordFinder.CLI.UnitTests
{
    public class MainCmdRequestValidatorTest
    {
        [Theory]
        [InlineData("a")]
        [InlineData("aa")]
        [InlineData("aaa")]
        [InlineData("aaaa")]
        [InlineData("aaaaa")]
        [InlineData("aaaaaa")]
        [InlineData("aaaaaaa")]
        [InlineData("aaaaaaaa")]
        public void LettersContainMax8CharactersValidationSuccess(string letters)
        {
            var request = new MainCmdRequest(default, letters, default);
            MainCmdRequestValidator validator = new();
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
        }

    }
}