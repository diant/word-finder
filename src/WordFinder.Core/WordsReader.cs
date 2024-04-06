using System.Reflection;

namespace WordFinder.Core;

public static class WordsReader
{
    private const string ResourceName = "WordFinder.Core.Dictionaries.sowpods.txt";
    private static IReadOnlyCollection<string> _words = new List<string>();

    public static async Task<IReadOnlyCollection<Word>> GetWordsAsync(
        int maxLen,
        string? contains = default, 
        string? startsWith = default, 
        string? endsWith = default)
    {
        if (_words.Count == 0) 
        {
            _words = await ListWordsAsync(maxLen);
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

    public static IReadOnlyCollection<Word> GetWords(
        int maxLen,
        string? contains = default,
        string? startsWith = default,
        string? endsWith = default)
    {
        if (_words.Count == 0)
        {
            _words = ListWords(maxLen);
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

    public static async Task<IReadOnlyCollection<string>> ListWordsAsync(int maxLen)
    {
        var allWords = await LoadWordsFromFile();
        return allWords
            .Split('\n', StringSplitOptions.TrimEntries)
            .Where(x => x.Length >= 2 && x.Length <= maxLen)
            .ToList();
    }   
   
    public static async Task<string> LoadWordsFromFile()
    {
        string result = string.Empty;
        using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName))
        using (var reader = new StreamReader(stream!))
        {
            result = await reader.ReadToEndAsync();
        }

        return result;
    }

    public static IReadOnlyCollection<string> ListWords(int maxLen) => LoadWordsFromFileAsEnumerable(maxLen).ToList();

    public static IEnumerable<string> LoadWordsFromFileAsEnumerable(int maxLen = 12)
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
