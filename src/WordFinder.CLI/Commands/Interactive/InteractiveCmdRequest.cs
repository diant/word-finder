using McMaster.Extensions.CommandLineUtils;
using MediatR;

namespace WordFinder.CLI.Commands.Interactive
{
    internal record InteractiveCmdRequest(CommandLineApplication App) : IRequest<int> { }
}
