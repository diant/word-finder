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
            var request = new MainCmdRequest(default, letters, 'l', default, default, default, 2);
            MainCmdRequestValidator validator = new();
            var result = validator.Validate(request);
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("#fsak")]
        [InlineData("diaman5d")]
        [InlineData("Lux3m")]
        [InlineData("@3f")]
        public void LettersMustContainOnlyCharacters(string letters)
        {
            var request = new MainCmdRequest(default, letters, default, default, default, default, 2);
            MainCmdRequestValidator validator = new();
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
        }

    }
}