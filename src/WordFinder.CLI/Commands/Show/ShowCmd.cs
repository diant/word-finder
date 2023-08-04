using McMaster.Extensions.CommandLineUtils;
using MediatR;

namespace WordFinder.CLI.Commands.Show
{
    [Command("show", Description = "Shows informational tips")]
    internal sealed class ShowCmd
    {
        public readonly IMediator _mediator;

        public ShowCmd(IMediator mediator) => _mediator = mediator;

        public Task<int> OnExecute(CommandLineApplication app) => _mediator.Send(new ShowCmdRequest(app));
    }
}
