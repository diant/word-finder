using FluentValidation;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using WordFinder.Core;

namespace WordFinder.CLI.Commands;

internal sealed class MainCmdHandler : IRequestHandler<MainCmdRequest, int>
{
    const string UNDERLINE = "\x1B[4m";
    const string BOLD = "\u001b[1m";
    const string YELLOW = "\u001b[33m";
    const string MAGENTA = "\x1b[35m";
    const string RESET = "\x1B[0m";
    const string SPACE = "  ";

    private readonly IConsole _console;
    private readonly IValidator<MainCmdRequest> validator;

    public MainCmdHandler(IConsole console, IValidator<MainCmdRequest> validator)
    {
        _console = console;
        this.validator = validator;
    }

    public Task<int> Handle(MainCmdRequest request, CancellationToken cancellationToken)
    {
        if (!request.Letters.Any())
        {
            request.App.ShowHelp();
            return Task.FromResult(0);
        }

        if (!ValidateRequest(request))
        {
            return Task.FromResult(0);
        }

        var grp = request.GroupBy == GroupBy.Length ? "letters" : request.GroupBy == 'p' ? "points" : "no grouping";
        _console.Write($"\nLetters: `{request.Letters}`\tGroup: `{grp}`");
        _console.Write($"\nContains: `{request.Contains}`\tStarts with: `{request.StartsWith}`\tEnds with: `{request.EndsWith}`");

        _console.WriteLine("\n--");

        var wordsFound = Core.WordFinder.Find(request.Letters, request.Contains, request.StartsWith, request.EndsWith, request.MinLen);
        PrintResults(request, grp, wordsFound);

        return Task.FromResult(0);
    }

    private void PrintResults(MainCmdRequest request, string grp, IReadOnlyCollection<Word> wordsFound)
    {
        if (request.GroupBy != GroupBy.None)
        {
            Func<Word, int> groupBy = request.GroupBy switch
            {
                'p' => x => x.Points,
                'l' => x => x.Length,
                _ => throw new NotImplementedException(),
            };

            foreach (var group in wordsFound.GroupBy(groupBy).OrderByDescending(x => x.Key))
            {
                _console.WriteLine($"\n{YELLOW}{BOLD}{group.Key} {grp}{RESET}");
                foreach (var word in group.OrderBy(x => x.Value))
                {
                    if (word.WildcardIndex >= 0)
                    {
                        for (int i = 0; i < word.Value.Length; i++)
                        {
                            if (i == word.WildcardIndex)
                            {
                                _console.Write("{0}{1}{2}{3}", MAGENTA, UNDERLINE, word.Value[i], RESET);
                            }
                            else
                            {
                                _console.Write("{0}{1}{2}", YELLOW, word.Value[i], RESET);
                            }
                        }
                        _console.Write(SPACE);
                    }
                    else
                    {
                        _console.Write("{0}{1}{2}{3}", YELLOW, word.Value, RESET, SPACE);
                    }
                }
            }
        }
        else
        {
            _console.WriteLine(string.Join(", ", wordsFound.OrderByDescending(x => x.Length).Select(x => x.Value)));
        }

        _console.WriteLine();
        _console.ResetColor();
    }

    private bool ValidateRequest(MainCmdRequest request)
    {
        var validatorResult = validator.Validate(request);
        if (!validatorResult.IsValid)
        {
            _console.ForegroundColor = ConsoleColor.Red;
            foreach (var error in validatorResult.Errors)
            {
                _console.WriteLine(error.ErrorMessage);
            }
            _console.ResetColor();
            request.App.ShowHint();

            return false;
        }

        return true;
    }
}
