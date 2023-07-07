using McMaster.Extensions.CommandLineUtils;
using MediatR;

namespace WordFinder.CLI.Commands.Interactive
{
    internal sealed class InteractiveCmdHandler : IRequestHandler<InteractiveCmdRequest, int>
    {
        private readonly IConsole _console;
        private readonly IMediator _mediator;

        public InteractiveCmdHandler(IConsole console, IMediator mediator)
        {
            _console = console;
            _mediator = mediator;
        }

        public async Task<int> Handle(InteractiveCmdRequest request, CancellationToken cancellationToken)
        {
            _console.ForegroundColor = ConsoleColor.Cyan;

            _console.WriteLine("Interactive mode. Click `q` to exit");
            _console.WriteLine("====================================");
            while (true)
            {
                _console.ForegroundColor = ConsoleColor.Cyan;
                _console.Write("Enter letters: ");
                var letters = _console.In.ReadLine();
                _console.WriteLine($"You entered: {letters}");
                if (letters == "q" || letters == "Q")
                {
                    _console.WriteLine("Exiting.. Bye bye!!");
                    _console.ResetColor();
                    return 0;
                }

                await _mediator.Send(new MainCmdRequest(request.App, letters, GroupBy.Length, default));
            }
        }
    }
}
