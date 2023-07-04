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
            var wordGroups = Core.WordFinder.Find(request.Letters, request.Grouped);
            if (request.Grouped)
            {
                foreach (var group in wordGroups.OrderByDescending(x => x.Key))
                {
                    _console.WriteLine($"\n{group.Key} letters");
                    _console.WriteLine("--------------------");
                    _console.WriteLine(string.Join(", ", group.Value));
                }
            }
            else
            {
                foreach(var word in wordGroups.Select(x => x.Value))
                {
                    _console.WriteLine(string.Join(", ", word));
                }
            }
        }

        return Task.FromResult(0);
    }
}
