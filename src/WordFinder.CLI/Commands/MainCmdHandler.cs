using FluentValidation;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using WordFinder.Core;

namespace WordFinder.CLI.Commands;

internal sealed class MainCmdHandler : IRequestHandler<MainCmdRequest, int>
{    
    private readonly IConsole _console;
    private readonly IValidator<MainCmdRequest> validator;

    public MainCmdHandler(IConsole console, IValidator<MainCmdRequest> validator)
    {
        _console = console;
        this.validator = validator;
    }

    public async Task<int> Handle(MainCmdRequest request, CancellationToken cancellationToken)
    {
        if (!request.Letters.Any())
        {
            request.App.ShowHelp();
            return 0;
        }

        if (!ValidateRequest(request))
        {
            return 0;
        }

        var grp = request.GroupBy == GroupBy.Length ? "letters" : request.GroupBy == 'p' ? "points" : "no grouping";
        _console.ForegroundColor = ConsoleColor.Gray;
        _console.Write($"Letters: `{request.Letters}`\tGroup: `{grp}`");
        if (!string.IsNullOrWhiteSpace(request.Contains))
        {
            _console.Write($"\nContains: `{request.Contains}`");
        }
        _console.ResetColor();
        _console.WriteLine();

        var wordsFound = await Core.WordFinder.Find(request.Letters, request.Contains);
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
                _console.BackgroundColor = ConsoleColor.DarkYellow;
                _console.ForegroundColor = ConsoleColor.Black;
                _console.WriteLine($"\n{group.Key} {grp}");
                _console.ResetColor();

                _console.ForegroundColor = ConsoleColor.DarkYellow;
                _console.WriteLine(string.Join(", ", group));
                _console.ResetColor();
            }
        }
        else
        {
            _console.ForegroundColor = ConsoleColor.DarkYellow;
            _console.WriteLine(string.Join(", ", wordsFound.OrderByDescending(x => x.Length).Select(x => x.Value)));
        }

        _console.ResetColor();
        return 0;
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
