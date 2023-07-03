using System.Reflection;

namespace WordFinder.Core;

public static class WordsReader
{
    private static IEnumerable<string> _words = new List<string>();

    public static IEnumerable<string> GetWords()
    {
        if (_words.Any())
        {
            return _words;
        }

        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "WordFinder.Core.words_alpha.txt";
        string result = string.Empty;
        using (var stream = assembly.GetManifestResourceStream(resourceName))
        using (var reader = new StreamReader(stream!))
        {
            result = reader.ReadToEnd();
        }
        _words = result.Split('\n', StringSplitOptions.TrimEntries).Where(x => x.Length >= 2).ToList();
        return _words;
    }
}
