using FluentValidation;
using McMaster.Extensions.CommandLineUtils;
using MediatR;

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

        if(!ValidateRequest(request))
        {
            return 0;
        }

        //_console.BackgroundColor = ConsoleColor.DarkCyan;
        _console.ForegroundColor = ConsoleColor.Gray;
        _console.WriteLine("WordFinder CLI tool\nFind all possible words for given letters");
        _console.WriteLine($"Letters: `{request.Letters}`\tGrouped: `{request.Grouped}`");
        _console.ResetColor();

        var wordGroups = await Core.WordFinder.Find(request.Letters, request.Grouped);
        if (request.Grouped)
        {
            foreach (var group in wordGroups.OrderByDescending(x => x.Key))
            {   
                _console.BackgroundColor = ConsoleColor.DarkYellow;
                _console.ForegroundColor = ConsoleColor.Black;
                _console.WriteLine($"\n{group.Key} letters");
                _console.ResetColor();

                _console.ForegroundColor = ConsoleColor.DarkYellow;
                //_console.WriteLine("--------------------");
                _console.WriteLine(string.Join(", ", group.Value));
                _console.ResetColor();
            }
        }
        else
        {
            _console.ForegroundColor = ConsoleColor.DarkYellow;
            foreach(var word in wordGroups.Select(x => x.Value).OrderByDescending(x => x.Length))
            {
                _console.WriteLine(string.Join(", ", word));
            }
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
            _console.WriteLine($"\nPlease try again");
            return false;
        }

        return true;
    }
}
