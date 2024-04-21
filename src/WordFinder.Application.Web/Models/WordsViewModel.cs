namespace WordFinder.Web.Models;

public record WordsViewModel(
    string? Letters, 
    IReadOnlyCollection<WordGroup> WordGroups, 
    int MinLen = 2)
{
    public string? Contains { get; set; } = string.Empty;
    public string? StartsWith { get; set; } = string.Empty;
    public string? EndsWith { get; set; } = string.Empty;
}

public record WordGroup(string Title, Word[] Words);

public record Word(string Value, int Points, int WildcardIdex, string DefinitionLink);