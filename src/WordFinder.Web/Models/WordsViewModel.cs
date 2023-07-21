namespace WordFinder.Web.Models;

public record WordsViewModel(string? Letters, IReadOnlyCollection<WordGroup> WordGroups);

public record WordGroup(string Title, Word[] Words);

public record Word(string Value, int Points)
{
    public override string ToString() => $"{Value}({Points})";
}