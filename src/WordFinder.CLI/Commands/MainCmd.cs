using McMaster.Extensions.CommandLineUtils;
using MediatR;
using WordFinder.CLI.Commands.Interactive;

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
    [HelpOption("-? | -h | --help")]
    [Subcommand(typeof(InteractiveCmd))]
    internal sealed class MainCmd
    {
        private readonly IMediator _mediator;
        public MainCmd(IMediator mediator) => _mediator = mediator;

        [Option(
            Description = "Input letters (max 10). Use `*` for wildcard (max 1)",
            ShortName = "l",
            LongName = "letters",
            ShowInHelpText = true,
            Inherited = true,
            ValueName = "LETTERS"
        )]
        public string Letters { get; set; } = string.Empty;

        [Option(
            Description = "Groups results by word length (default), points or no grouping",
            ShortName = "g",
            LongName = "group",
            ValueName = "l (length) | p (points) | n (no grouping)",
            ShowInHelpText = true,
            Inherited = true
        )]
        public char Group { get; set; } = GroupBy.Length;

        [Option(            
        Description = "Shows only words that contain the given string",
                       ShortName = "c",
                       LongName = "contains",
                       ShowInHelpText = true,
                       Inherited = true,
                       ValueName = "SUBSTRING"
                   )]
        public string Contains { get; set; } = default;

        [Option(
        Description = "Shows only words that start with the given string",
                       ShortName = "s",
                       LongName = "starts-with",
                       ShowInHelpText = true,
                       Inherited = true,
                       ValueName = "SUBSTRING"
                   )]
        public string StartsWith { get; set; } = default;

        [Option(
        Description = "Shows only words that end with the given string",
                       ShortName = "e",
                       LongName = "ends-with",
                       ShowInHelpText = true,
                       Inherited = true,
                       ValueName = "SUBSTRING"
                   )]
        public string EndsWith { get; set; } = default;

        [Option(
        Description = "Minimum word length (default 4)",
                       ShortName = "ml",
                       LongName = "min-len",
                       ShowInHelpText = true,
                       Inherited = true
                   )]
        public int MinLen { get; set; } = 4;

        public Task<int> OnExecute(CommandLineApplication app) => 
            _mediator.Send(new MainCmdRequest(app, Letters, Group, Contains, StartsWith, EndsWith, MinLen));

        private static string GetVersion() => typeof(MainCmd).Assembly.GetName().Version!.ToString();
    }

    public class GroupBy
    {
        public static readonly char Length = 'l';
        public static readonly char Points = 'p';
        public static readonly char None = 'n';

        public override string ToString() => "l (length) | p (points) | n (no grouping)";
    }
}
