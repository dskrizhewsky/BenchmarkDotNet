#region Copyright Notice

// ----------------------------------------------------------------------------
// <copyright file="StringConcatenationBenchmark.cs" company="Dmytro Skryzhevskyi">
// </copyright>
// ----------------------------------------------------------------------------

#endregion

using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace Benchmarks;

[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
[RyuJitX64Job]
[RyuJitX86Job]
[SimpleJob(RunStrategy.ColdStart, launchCount: 1, warmupCount: 5, iterationCount: 5, id: "FastAndDirtyJob")]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[MemoryDiagnoser()]
public class StringConcatenationBenchmark
{
    private string str1;
    private string str2;
    private string str3;
    private string[] _words;

    public void Print()
    {
        Console.WriteLine(FormatBenchmark());
        Console.WriteLine(InterpolationBenchmark());
        Console.WriteLine(StringBuilderBenchmark());
        Console.WriteLine(PlusBenchmark());
        Console.WriteLine(ConcatBenchmark());
        Console.WriteLine(JoinBenchmark());
        Console.WriteLine(AggregateBenchmark());
    }

    [GlobalSetup]
    public void Setup()
    {
        str1 = "dfsfsdfsfsfsdfsdfsfdsfsdfsdf";
        str2 = "fdsfsdfsdfsdfsdfsdfsdfsdfsfsfsfsfsfsdfsf";
        str3 = "yyjk,juk,k,dfsfsdfsfsdfsfswerwerwerwerwerwerwerwrewr";
        _words = new[] { str1, str2, str3 };
    }

    [Benchmark()]
    public string FormatBenchmark()
    {
        return string.Format("{0}{1}{2}", str1, str2, str3);
    }

    [Benchmark()]
    public string InterpolationBenchmark()
    {
        return $"{str1}{str2}{str3}";
    }

    [Benchmark()]
    public string StringBuilderBenchmark()
    {
        var sb = new StringBuilder();
        sb.Append(str1);
        sb.Append(str2);
        sb.Append(str3);
        return sb.ToString();
    }

    [Benchmark()]
    public string StringBuilderCacheBenchmark()
    {
        var sb = StringBuilderCache.Acquire();
        sb.Append(str1);
        sb.Append(str2);
        sb.Append(str3);
        return StringBuilderCache.GetStringAndRelease(sb);
    }

    [Benchmark()]
    public string PlusBenchmark()
    {
        return str1 + str2 + str3;
    }

    [Benchmark()]
    public string ConcatBenchmark()
    {
        return string.Concat(str1, str2, str3);
    }

    [Benchmark()]
    public string JoinBenchmark()
    {
        return string.Join("", _words);
    }

    [Benchmark()]
    public string AggregateBenchmark()
    {
        return _words.Aggregate((x, y) => $"{x}{y}");
    }
}