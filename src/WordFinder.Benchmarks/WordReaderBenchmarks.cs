using BenchmarkDotNet.Attributes;

namespace WordFinder.Benchmarks
{
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    [ThreadingDiagnoser]
    public class WordReaderBenchmarks
    {
        [Benchmark]
        public void ListAllWordsFromFile() => Core.WordsReader.ListWords(7);
    }
}
