using McMaster.Extensions.CommandLineUtils;
using MediatR;
using WordFinder.Core;

namespace WordFinder.CLI.Commands;

public record MainCmdRequest(CommandLineApplication App, string Letters, bool Grouped, string Contains) 
    : IRequest<int>;
