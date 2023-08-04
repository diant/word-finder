using BenchmarkDotNet.Attributes;

namespace WordFinder.Benchmarks
{
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    [ThreadingDiagnoser]
    public class WordReaderBenchmarks
    {
        //[Benchmark]
        //public async Task ReadWordsWithoutContainsAsync() => await Core.WordsReader.GetWordsAsync(7);
        //[Benchmark]
        //public void ReadWordsWithoutContains() => Core.WordsReader.GetWords(7);

        //[Benchmark]
        //public async Task ReadWordsContainingOneLetterAsync() => await Core.WordsReader.GetWordsAsync("e");

        //[Benchmark]
        //public void ReadWordsContainingOneLetter() => Core.WordsReader.GetWords("e");

        [Benchmark]
        public async Task ListAllWordsFromFileAsync() => await Core.WordsReader.ListWordsAsync(7);

        [Benchmark]
        public void ListAllWordsFromFile() => Core.WordsReader.ListWords(7);

        //[Benchmark]
        //public async Task ReadWordsFromFile() => await Core.WordsReader.LoadWordsFromFile();
    }
}
