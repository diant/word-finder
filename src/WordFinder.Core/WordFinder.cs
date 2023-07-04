namespace WordFinder.Core;

public static class WordFinder
{
    public static async Task<Dictionary<int,string[]>> Find(string letters, bool grouped)
    {
        var words = await WordsReader.GetWords();
        var finalResult = FindWords(words, letters);

        if (grouped)
        {
            return GroupwordsByLength(finalResult);
        }
        return new Dictionary<int, string[]> { { 0, finalResult.ToArray() } };
    }

    private static Dictionary<int, string[]> GroupwordsByLength(IEnumerable<string> words)
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

