using BenchmarkDotNet.Attributes;

namespace WordFinder.Benchmarks
{
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    [ThreadingDiagnoser]
    public class WordFinderBenchmarks
    {
        const string Letters = "unhilar";

        [Benchmark]
        public void FindWordsWithLetters() => Core.WordFinder.Find(Letters);
    }
}
