namespace WordFinder.Core;

public static class WordFinder
{
    public static async Task<IReadOnlyCollection<Word>> FindWithQ(bool withU = true)
    {
        var words = await WordsReader.GetWordsAsync(40, "q");
        if (withU)
        {
            words = words.Where(w => w.Value.Contains('u', StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        else
        {
            words = words.Where(w => !w.Value.Contains('u', StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        return words;
    }

    [Obsolete("Please use the Find method. It's more efficient")]
    public static async Task<IReadOnlyCollection<Word>> FindAsync(
        string letters, 
        string? contains = default,
        string? startsWith = default,
        string? endsWith = default,
        int minLen = 2)
    {
        var words = await WordsReader.GetWordsAsync(letters.Length, contains, startsWith, endsWith);
        var result = letters.Contains('*') ?
            FindWordsWithWildCard(words, letters, minLen) :
            FindWords(words, letters, minLen);

        return result;
    }

    public static IReadOnlyCollection<Word> Find(
        string letters,
        string? contains = default,
        string? startsWith = default,
        string? endsWith = default,
        int minLen = 2)
    {
        var words = WordsReader.GetWords(letters.Length, contains, startsWith, endsWith);
        var result = letters.Contains('*') ?
            FindWordsWithWildCard(words, letters, minLen) :
            FindWords(words, letters, minLen);

        return result;
    }

    public static bool ContainsAtLeastOne(this string s, char[] chars)
    {
        foreach (var c in s)
            if (chars.Contains(c)) return true;
        return false;
    }

    private static IReadOnlyCollection<Word> FindWords(IEnumerable<Word> words, string letters, int minLen = 2)
    {
        List<Word> result = new();

        foreach (var word in words
            .Where(x => x.Value.ContainsAtLeastOne(letters.ToCharArray()) && x.Length >= minLen && x.Length <= letters.Length))
        {
            var temp = letters;
            var found = true;
            foreach (var c in word.Value)
            {
                var index = temp.IndexOf(c, StringComparison.InvariantCultureIgnoreCase);
                if (index == -1)
                {
                    found = false;
                    break;
                }
                temp = temp.Remove(index, 1);
            }
            if (found)
            {
                var points = word.Value.ToUpperInvariant().Sum(c => WordsReader.LetterPoints[c]);
                word.UpdatePoints(points);
                result.Add(word);
            }
        }

        return result;
    }

    private static IReadOnlyCollection<Word> FindWordsWithWildCard(IEnumerable<Word> words, string letters, int minLen = 2)
    {
        var result = new List<Word>();
        var wildcardCount = letters.Count(c => c == '*');
        var letterCounts = letters.Where(c => c != '*').GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

        foreach (var word in words.Where(x => x.Length >= minLen))
        {
            var wordLetterCounts = word.Value.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            bool isMatch = true;
            int wildcardsUsed = 0;

            foreach (var kvp in wordLetterCounts)
            {
                if (letterCounts.ContainsKey(kvp.Key))
                {
                    if (kvp.Value > letterCounts[kvp.Key])
                    {
                        int difference = kvp.Value - letterCounts[kvp.Key];
                        wildcardsUsed += difference;
                        if (wildcardsUsed > wildcardCount)
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    //word.Wildchar = kvp.Key;
                }
                else
                {
                    wildcardsUsed += kvp.Value;
                    if (wildcardsUsed > wildcardCount)
                    {
                        isMatch = false;
                        break;
                    }
                    word.Wildchar = kvp.Key;
                }
            }

            if (isMatch)
            {
                var points = word.Value.ToUpperInvariant().Sum(c => WordsReader.LetterPoints[c]);
                if (word.Wildchar != null)
                {
                    points -= WordsReader.LetterPoints[word.Wildchar.Value];
                }
                word.UpdatePoints(points);
                result.Add(word);
            }
        }

        return result;
    }
}

