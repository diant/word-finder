﻿namespace WordFinder.Web.Models;

public record WordsViewModel(
    string? Letters, 
    IReadOnlyCollection<WordGroup> WordGroups, 
    int MinLen = 2)
{
    public string? Contains { get; set; } = default;
    public string? StartsWith { get; set; } = default;
    public string? EndsWith { get; set; } = default;
}

public record WordGroup(string Title, Word[] Words);

public record Word(string Value, int Points, int WildcardIdex, string DefinitionLink);