#region Copyright Notice
/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2023 Dmytro Skryzhevskyi
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
*/
#endregion


using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace Benchmarks;

[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
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