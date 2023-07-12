using FluentValidation;

namespace WordFinder.CLI.Commands;

public class MainCmdRequestValidator : AbstractValidator<MainCmdRequest>
{
    private const int MaxLettersLength = 10;
    private readonly char[] GroupByAllowedValues = new char[] { 'l', 'p', 'n' };
    private const string LettersOnlyRegex = @"^[a-zA-Z]+$";
    private const string LettersWithWildcardRegex = @"^[a-zA-Z\*]+$";

    public MainCmdRequestValidator()
    {
        RuleFor(x => x.Letters)
            .MaximumLength(MaxLettersLength)
            .WithMessage($"Letters must be maximum {MaxLettersLength} characters");
        //RuleFor(x => x.Letters)
        //    .Matches(LettersOnlyRegex)
        //    .WithMessage("Letters must contain only characters");
        RuleFor(x => x.GroupBy)
            .Must(x => GroupByAllowedValues.Contains(x))
            .WithMessage("GroupBy must be one of the following: l (length) | p (points) | n (no grouping)");
        RuleFor(x => x.Contains)
            .Matches(LettersOnlyRegex)
            .WithMessage("Contains must contain only characters");
        RuleFor(x => x.Letters)
            .Matches(LettersWithWildcardRegex)
            .WithMessage("Letters must contain only letters and wildcard `*`");
        RuleFor(x => x.Letters)
            .Must(x => x.Count(c => c == '*') <= 1)
            .WithMessage("Letters must contain only one wildcard `*`");
    }
}
