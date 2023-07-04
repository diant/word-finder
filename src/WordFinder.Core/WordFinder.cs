namespace WordFinder.Core;

public static class WordFinder
{
    public static async Task<Dictionary<int,string[]>> Find(
        string letters, 
        bool grouped = true, 
        string? contains = default)
    {
        var words = await WordsReader.GetWords();
        var result = FindWords(words, letters);

        if (!string.IsNullOrEmpty(contains))
        {
            result = result.Where(w => w.Contains(contains)).ToList();
        }

        if (grouped)
        {
            return GroupWordsByLength(result);
        }
        return new Dictionary<int, string[]> { { 0, result.ToArray() } };
    }

    private static Dictionary<int, string[]> GroupWordsByLength(IEnumerable<string> words)
    {
        return words.GroupBy(w => w.Length).ToDictionary(g => g.Key, g => g.Select(g => string.Join(", ", g)).ToArray());
    }

    private static List<string> FindWords(IReadOnlyCollection<string> words, string letters)
    {
        List<string> result = new();
        foreach (string word in words)
        {
            if (word.Length < 2) continue;
            string temp = letters;
            bool found = true;
            foreach (char c in word)
            {
                int index = temp.IndexOf(c);
                if (index == -1)
                {
                    found = false;
                    break;
                }
                temp = temp.Remove(index, 1);
            }
            if (found) result.Add(word);
        }
        return result;
    }

}

