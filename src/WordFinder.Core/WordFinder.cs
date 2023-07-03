namespace WordFinder.Core;

public static class WordFinder
{
    public static string[] Find(string letters, bool grouped = false)
    {
        var words = WordsReader.GetWords();
        var matchedWords = new List<string>();
        var wordsWithCorrectLen = words.Where(x => x.Length <= letters.Length).ToList();
        foreach (var word in wordsWithCorrectLen)
        {
            var wordLetters = word.ToCharArray();
            var found = true;
            foreach (var letter in wordLetters)
            {
                if (!letters.Contains(letter))
                {
                    found = false;
                    break;
                }
            }
            if (found)
            {
                matchedWords.Add(word);
            }
        }
        var finalResult = new List<string>(matchedWords);


        if (grouped)
        {
            finalResult = finalResult.GroupBy(w => w.Length).Select(g => string.Join(", ", g)).ToList();
        }
        return finalResult.ToArray();
    }
}

