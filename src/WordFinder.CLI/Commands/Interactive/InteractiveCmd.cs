using McMaster.Extensions.CommandLineUtils;
using MediatR;

namespace WordFinder.CLI.Commands.Interactive
{
    [Command("i", Description = "Interactive mode")]
    internal sealed class InteractiveCmd
    {
        private readonly IMediator _mediator;
        public InteractiveCmd(IMediator mediator) => _mediator = mediator;

        public Task<int> OnExecute(CommandLineApplication app) => _mediator.Send(new InteractiveCmdRequest(app));
    }
}
