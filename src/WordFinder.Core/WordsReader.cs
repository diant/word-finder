using System.Reflection;

namespace WordFinder.Core;

public static class WordsReader
{
    private const string ResourceName = "WordFinder.Core.words_alpha.txt";
    private static IReadOnlyCollection<string> _words = new List<string>();

    public static async Task<IReadOnlyCollection<string>> GetWords()
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
        _words = result.Split('\n', StringSplitOptions.TrimEntries).Where(x => x.Length >= 2).ToList();
        return _words;
    }
}
