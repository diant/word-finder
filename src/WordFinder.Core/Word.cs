namespace WordFinder.Core;

public record Word(string Value, int Length)
{
    public int Points { get; private set; } = Value is null ? 0 : Value.Sum(c => WordsReader.LetterPoints[c]);
    public int WildcardIndex { get; set; } = -1;

    public override string ToString() => WildcardIndex >= 0 ? $"{Value}({Value[WildcardIndex]}*)" : Value;
    internal void UpdatePoints(int points) => Points = points;
}
