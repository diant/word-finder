using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WordFinder.Web.Models;

public record WordsViewModel(
    string? Letters, 
    IReadOnlyCollection<WordGroup> WordGroups, 
    int MinLen = 2, 
    string? Contains = default, 
    string? StartsWith = default,
    string? EndsWith = default);

public record WordGroup(string Title, Word[] Words);

public record Word(string Value, int Points, string DefinitionLink)
{
    public override string ToString() => $"{Value}({Points})";
}