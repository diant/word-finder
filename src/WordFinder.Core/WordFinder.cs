namespace WordFinder.Core;

public static class WordFinder
{
    public static async Task<IReadOnlyCollection<Word>> Find(
        string letters, 
        string? contains = default)
    {
        var words = await WordsReader.GetWords(contains);
        var result = FindWords(words, letters);

        return result;
    }

    private static IReadOnlyCollection<Word> FindWords(IEnumerable<Word> words, string letters)
    {
        List<Word> result = new();
        foreach (var word in words)
        {
            if (word.Length < 2) continue;
            string temp = letters;
            bool found = true;
            foreach (char c in word.Value)
            {
                int index = temp.IndexOf(c, StringComparison.InvariantCultureIgnoreCase);
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

