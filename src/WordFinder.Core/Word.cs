namespace WordFinder.Core;

public record Word(string Value, int Length)
{
    public int Points { get; private set; } = 0;
    public char? Wildchar { get; set; } = default;

    public override string ToString() => Wildchar is null ? Value : $"{Value}(*{Wildchar})";
    public void UpdatePoints(int points) => Points = points;
}
