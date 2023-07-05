using System.Reflection;

namespace WordFinder.Core;

public static class WordsReader
{
    //Tile Values
    //Below are the point values for each letter that is used in a Scrabble game.

    //0 Points - Blank tile.
    //1 Point - A, E, I, L, N, O, R, S, T and U.
    //2 Points - D and G.
    //3 Points - B, C, M and P.
    //4 Points - F, H, V, W and Y.
    //5 Points - K.
    //8 Points - J and X.
    //10 Points - Q and Z.
    private readonly static Dictionary<char, int> LetterPoints = new()
    {
        { 'A', 1 },
        { 'B', 3 },
        { 'C', 3 },
        { 'D', 2 },
        { 'E', 1 },
        { 'F', 4 },
        { 'G', 2 },
        { 'H', 4 },
        { 'I', 1 },
        { 'J', 8 },
        { 'K', 5 },
        { 'L', 1 },
        { 'M', 3 },
        { 'N', 1 },
        { 'O', 1 },
        { 'P', 3 },
        { 'Q', 10 },
        { 'R', 1 },
        { 'S', 1 },
        { 'T', 1 },
        { 'U', 1 },
        { 'V', 4 },
        { 'W', 4 },
        { 'X', 8 },
        { 'Y', 4 },
        { 'Z', 10 }
    };

    private const string ResourceName = "WordFinder.Core.sowpods.txt";
    private static IReadOnlyCollection<Word> _words = new List<Word>();

    public static async Task<IReadOnlyCollection<Word>> GetWords(string? contains = default)
    {
        if (_words.Any())
        {
            return _words;
        }

        var assembly = Assembly.GetExecutingAssembly();
        string result = string.Empty;
        using (var stream = assembly.GetManifestResourceStream(ResourceName))
        using (var reader = new StreamReader(stream!))
        {
            result = await reader.ReadToEndAsync();
        }

        Func<string, bool> filter = x => x.Length >= 2;
        if (!string.IsNullOrWhiteSpace(contains))
        {
            filter = x => x.Length >= 2 && x.Contains(contains, StringComparison.InvariantCultureIgnoreCase);
        }

        _words = result
            .Split('\n', StringSplitOptions.TrimEntries)
            .Where(filter)
            .Select(x => new Word(x.ToLower(), x.Length, x.Sum(c => LetterPoints[c])))
            .ToList();

        return _words;
    }
}
