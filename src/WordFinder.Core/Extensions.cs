namespace WordFinder.Core;

public static class Extensions
{
    public static Word MapToWord(this string s) => new(s.ToLower(), s.Length);
}
