using McMaster.Extensions.CommandLineUtils;
using MediatR;

namespace WordFinder.CLI.Commands;

internal sealed class MainCmdHandler : IRequestHandler<MainCmdRequest, int>
{    
    private readonly IConsole _console;

    public MainCmdHandler(IConsole console)
    {
        _console = console;
    }

    public Task<int> Handle(MainCmdRequest request, CancellationToken cancellationToken)
    {
        if(!request.Letters.Any())
        {
            request.App.ShowHelp();
        }
        else
        {

            _console.WriteLine("WordFinder CLI tool\nFind all possible words for given letters");
            _console.WriteLine($"Letters: `{request.Letters}`\tGrouped: `{request.Grouped}`");
            var words = Core.WordFinder.Find(request.Letters, request.Grouped);
            foreach (var word in words)
            {
                _console.WriteLine(word);
            }
        }

        return Task.FromResult(0);
    }
}
