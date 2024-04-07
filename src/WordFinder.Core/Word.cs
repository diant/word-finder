namespace WordFinder.Core;

public sealed record Word(string Value, int Length)
{
    public int Points { get; private set; } = 
        string.IsNullOrWhiteSpace(Value) ? 0 : Value.Sum(c => LetterPoints.Points_SOWPODS[c]);

    public int WildcardIndex { get; set; } = -1;

    public override string ToString() => WildcardIndex >= 0 ? $"{Value}({Value[WildcardIndex]}*)" : Value;

    internal void UpdatePoints(int points) => Points = points;
}
