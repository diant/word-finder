﻿using FluentValidation;
using McMaster.Extensions.CommandLineUtils;
using MediatR;

namespace WordFinder.CLI.Commands;

internal sealed class MainCmdHandler : IRequestHandler<MainCmdRequest, int>
{    
    private readonly IConsole _console;
    private readonly IValidator<MainCmdRequest> validator;

    public MainCmdHandler(IConsole console, IValidator<MainCmdRequest> validator)
    {
        _console = console;
        this.validator = validator;
    }

    public Task<int> Handle(MainCmdRequest request, CancellationToken cancellationToken)
    {
        if (!request.Letters.Any())
        {
            request.App.ShowHelp();
            return Task.FromResult(0);
        }

        if(!ValidateRequest(request))
        {
            return Task.FromResult(0);
        }
        
        _console.WriteLine("WordFinder CLI tool\nFind all possible words for given letters");
        _console.WriteLine($"Letters: `{request.Letters}`\tGrouped: `{request.Grouped}`");
        var wordGroups = Core.WordFinder.Find(request.Letters, request.Grouped);
        if (request.Grouped)
        {
            foreach (var group in wordGroups.OrderByDescending(x => x.Key))
            {
                _console.WriteLine($"\n{group.Key} letters");
                _console.WriteLine("--------------------");
                _console.WriteLine(string.Join(", ", group.Value));
            }
        }
        else
        {
            foreach(var word in wordGroups.Select(x => x.Value))
            {
                _console.WriteLine(string.Join(", ", word));
            }
        }

        return Task.FromResult(0);
    }

    private bool ValidateRequest(MainCmdRequest request)
    {
        var validatorResult = validator.Validate(request);
        if (!validatorResult.IsValid)
        {
            _console.ForegroundColor = ConsoleColor.Red;
            foreach (var error in validatorResult.Errors)
            {
                _console.WriteLine(error.ErrorMessage);
            }
            _console.ResetColor();
            _console.WriteLine($"\nPlease try again");
            return false;
        }

        return true;
    }
}
