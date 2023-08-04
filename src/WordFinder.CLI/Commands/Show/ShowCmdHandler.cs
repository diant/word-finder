using McMaster.Extensions.CommandLineUtils;
using MediatR;
using WordFinder.Core;

namespace WordFinder.CLI.Commands.Show
{
    internal sealed class ShowCmdHandler : IRequestHandler<ShowCmdRequest, int>
    {
        private readonly IConsole _console;

        public ShowCmdHandler(IConsole console) => _console = console;

        public Task<int> Handle(ShowCmdRequest request, CancellationToken cancellationToken)
        {
            var points = LetterPoints.GetPoints_SOWPODS();
            _console.WriteLine("\nLetter points with SOWPODS dictionary:\n");
            foreach (var point in points.OrderBy(x => x.Key))
            {
                _console.Write($"{point.Key}:{point.Value} ");
            }
            _console.WriteLine();

            return Task.FromResult(0);
        }
    }
}
