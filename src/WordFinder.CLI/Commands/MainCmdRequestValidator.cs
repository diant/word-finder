using FluentValidation;

namespace WordFinder.CLI.Commands;

public class MainCmdRequestValidator : AbstractValidator<MainCmdRequest>
{
    private const int MaxLettersLength = 8;
    private readonly char[] GroupByAllowedValues = new char[] { 'l', 'p', 'n' };

    public MainCmdRequestValidator()
    {
        RuleFor(x => x.Letters)
            .MaximumLength(MaxLettersLength)
            .WithMessage($"Letters must be maximum {MaxLettersLength} characters");
        RuleFor(x => x.Letters)
            .Matches(@"^[a-zA-Z]+$")
            .WithMessage("Letters must contain only characters");
        RuleFor(x => x.GroupBy)
            .Must(x => GroupByAllowedValues.Contains(x))
            .WithMessage("GroupBy must be one of the following: l (length) | p (points) | n (no grouping)");
        //RuleFor(x => x.Letters)
        //    .Matches(@"^[a-zA-Z\*]+$")
        //    .WithMessage("Letters must contain only letters and wildcard `*`");
        //RuleFor(x => x.Letters)
        //    .Must(x => x.Count(c => c == '*') <= 1)
        //    .WithMessage("Letters must contain only one wildcard `*`");
    }
}
