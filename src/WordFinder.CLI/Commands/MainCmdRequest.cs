using McMaster.Extensions.CommandLineUtils;
using MediatR;

namespace WordFinder.CLI.Commands;

public record MainCmdRequest(CommandLineApplication App, string Letters, bool Grouped) : IRequest<int>;
