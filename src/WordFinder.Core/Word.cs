namespace WordFinder.Core;

public record Word(string Value, int Length)
{
    public int Points { get; private set; } = Value is null ? 0 : Value.Sum(c => WordsReader.LetterPoints[c]);
    public char? Wildchar { get; set; } = default;

    public override string ToString() => Wildchar is null ? Value : $"{Value}(*{Wildchar})";
    internal void UpdatePoints(int points) => Points = points;
}
