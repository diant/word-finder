﻿using McMaster.Extensions.CommandLineUtils;
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
            Description = "Groups the results by word length (Default `true`)",
            ShortName = "g",
            LongName = "grouped",
            ShowInHelpText = true,
            Inherited = true
        )]
        public bool Grouped { get; set; } = true;

        public Task<int> OnExecute(CommandLineApplication app) => _mediator.Send(new MainCmdRequest(app, Letters, Grouped));

        private static string GetVersion() => typeof(MainCmd).Assembly.GetName().Version!.ToString();
    }
}