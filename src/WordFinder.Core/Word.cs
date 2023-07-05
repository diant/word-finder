namespace WordFinder.Core;

public record Word(string Value, int Length, int Points)
{
    public override string ToString() => $"{Value}";
}
