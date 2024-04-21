using System.Reflection;

namespace WordFinder.Core;

public static class DictionaryLoader
{
    private const string ResourceName = "WordFinder.Core.Dictionaries.sowpods.txt";
    private static IReadOnlyCollection<string> _words = new List<string>();

    public static IReadOnlyCollection<Word> GetWords(
        int maxLen,
        string? contains = default,
        string? startsWith = default,
        string? endsWith = default)
    {
        if (_words.Count == 0)
        {
            _words = LoadWordsFromFileAsEnumerable(maxLen).ToList();
        }

        Func<string, bool> containsFilter = x => true;
        Func<string, bool> startsWithFilter = x => true;
        Func<string, bool> endsWithFilter = x => true;

        if (!string.IsNullOrWhiteSpace(contains))
        {
            containsFilter = x => x.Contains(contains, StringComparison.InvariantCultureIgnoreCase);
        }
        if (!string.IsNullOrWhiteSpace(startsWith))
        {
            startsWithFilter = x => x.StartsWith(startsWith, StringComparison.InvariantCultureIgnoreCase);
        }
        if (!string.IsNullOrWhiteSpace(endsWith))
        {
            endsWithFilter = x => x.EndsWith(endsWith, StringComparison.InvariantCultureIgnoreCase);
        }

        return _words
            .Where(containsFilter)
            .Where(startsWithFilter)
            .Where(endsWithFilter)
            .Select(x => x.MapToWord())
            .ToList();
    }

    private static IEnumerable<string> LoadWordsFromFileAsEnumerable(int maxLen = 12)
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName);
        using var reader = new StreamReader(stream!);
        string? line = default;
        
        while ((line = reader.ReadLine()) != null)
        {
            if (line.Length >= 2 && line.Length <= maxLen) yield return line.Trim();
        }
    }
}
