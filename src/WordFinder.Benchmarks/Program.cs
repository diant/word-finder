using BenchmarkDotNet.Running;
using WordFinder.Benchmarks;

var summary = BenchmarkRunner.Run<Benchmark>();