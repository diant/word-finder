using BenchmarkDotNet.Running;
using WordFinder.Benchmarks;

if (args != null && args.Length == 1 && args[0].Equals("f", StringComparison.InvariantCultureIgnoreCase))
{
    Console.WriteLine("Running benchmarks for WordFinder");
    BenchmarkRunner.Run<WordFinderBenchmarks>();
}
else 
{
    Console.WriteLine("Running benchmarks for WordReader");
    BenchmarkRunner.Run<WordReaderBenchmarks>();
}