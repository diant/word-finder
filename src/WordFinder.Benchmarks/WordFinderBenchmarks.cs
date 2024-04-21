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

        [Benchmark]
        public void FindWordsMinLen5() => Core.WordFinder.Find(Letters, minLen: 5);
        [Benchmark]
        public void FindWordsMinLen7() => Core.WordFinder.Find(Letters, minLen: 7);
    }
}
