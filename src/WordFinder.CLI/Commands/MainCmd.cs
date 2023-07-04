using McMaster.Extensions.CommandLineUtils;
using MediatR;

namespace WordFinder.CLI.Commands
{
    [Command("wfind",
        Description = @$"
*************************************************
*   WordFinder CLI tool                         *
*   Find all possible words for given letters   *
*************************************************
"
    )]
    [VersionOptionFromMember("--version", MemberName = nameof(GetVersion))]
    internal sealed class MainCmd
    {
        private readonly IMediator _mediator;
        public MainCmd(IMediator mediator) => _mediator = mediator;

        [Option(
            Description = "Input letters (max 8)",
            ShortName = "l",
            LongName = "letters",
            ShowInHelpText = true,
            Inherited = true,
            ValueName = "LETTERS"
        )]
        public string Letters { get; set; } = string.Empty;

        [Option(
            Description = "Shows the result without grouping by word length",
            ShortName = "ng",
            LongName = "no-group",
            ShowInHelpText = true,
            Inherited = true
        )]
        public bool NoGroup { get; set; } = false;

        [Option(            
        Description = "Shows only words that contain the given string",
                       ShortName = "c",
                       LongName = "contains",
                       ShowInHelpText = true,
                       Inherited = true,
                       ValueName = "SUBSTRING"
                   )]
        public string Contains { get; set; } = default;

        public Task<int> OnExecute(CommandLineApplication app) => _mediator.Send(new MainCmdRequest(app, Letters, !NoGroup, Contains));

        private static string GetVersion() => typeof(MainCmd).Assembly.GetName().Version!.ToString();
    }
}
