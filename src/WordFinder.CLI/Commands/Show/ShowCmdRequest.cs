using McMaster.Extensions.CommandLineUtils;
using MediatR;

namespace WordFinder.CLI.Commands.Show
{
    internal record ShowCmdRequest(CommandLineApplication App) : IRequest<int>;
}
