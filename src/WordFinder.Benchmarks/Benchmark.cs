using BenchmarkDotNet.Attributes;

namespace WordFinder.Benchmarks
{
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    [ThreadingDiagnoser]
    public class Benchmark
    {
        [Benchmark]
        public async Task ReadWordsWithoutContains()=> await Core.WordsReader.GetWords();

        [Benchmark]
        public async Task ReadWordsContainingOneLetter() => await Core.WordsReader.GetWords("e");

    }
}
